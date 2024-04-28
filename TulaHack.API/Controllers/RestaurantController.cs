using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using TulaHack.API.Contracts;
using TulaHack.Application.Services;
using TulaHack.Core.Models;

namespace TulaHack.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantsService _restaurantsService;
        private readonly SchemesService _schemesService;
        private readonly BookingsService _bookingsService;

        public RestaurantController(RestaurantsService restaurantsService, SchemesService schemesService, BookingsService bookingsService)
        {
            _restaurantsService = restaurantsService;
            _schemesService = schemesService;
            _bookingsService = bookingsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<RestaurantResponse>>> GetRestaurants()
        {
            var restaurants = await _restaurantsService.Get();

            var response = restaurants
                .Select(r => new RestaurantResponse(
                    r.Id,
                    r.Title,
                    r.Subtitle,
                    r.Description,
                    r.UserId,
                    new UserResponse(
                        r.User.Id,
                        r.User.Login,
                        r.User.Role,
                        r.User.FirstName,
                        r.User.LastName,
                        r.User.MiddleName,
                        r.User.Phone
                        ),
                    r.Address,
                    r.Kitchen,
                    r.MenuIds,
                    r.Photos,
                    r.Raiting,
                    r.StartWorkTime,
                    r.EndWorkTime,
                    r.SchemeId
                    ));

            return Ok(response);
        }

        [HttpGet("{id:guid}/Table/{date}")]
        public async Task<ActionResult<List<FreeTablesResponse>>> GetFreeTables(Guid id, string? date)
        {
            var restaurant = await _restaurantsService.GetById(id);

            if (restaurant == null) return null;

            var searchDate = (!string.IsNullOrEmpty(date)) ? DateTime.Parse(date) : new DateTime();
            var bookings = await _bookingsService.GetBookingsByDate(restaurant.SchemeId, searchDate);

            var response = new List<FreeTablesResponse>();

            var slotTime = TimeOnly.Parse(restaurant.StartWorkTime);
            var endTime = TimeOnly.Parse(restaurant.EndWorkTime);
            while (slotTime < endTime)
            {
                var tableIds = new List<Guid>();
                foreach (var booking in bookings)
                {
                    if (slotTime > TimeOnly.Parse(booking.StartTime) && slotTime < TimeOnly.Parse(booking.EndTime))
                    {
                        tableIds.Add(booking.TableId);
                    }
                }

                response.Add(new FreeTablesResponse(
                    tableIds,
                    DateOnly.FromDateTime(DateTime.Now).ToString(),
                    slotTime.ToString()
                    )
                );

                slotTime = slotTime.AddMinutes(60);
            }

            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<RestaurantResponse>> GetRestaurantById(Guid id)
        {
            var restaurant = await _restaurantsService.GetById(id);

            if (restaurant == null) return BadRequest("Restaurant not found");

            var user = new UserResponse(
                restaurant.User.Id,
                restaurant.User.Login,
                restaurant.User.Role,
                restaurant.User.FirstName,
                restaurant.User.LastName,
                restaurant.User.MiddleName,
                restaurant.User.Phone
                );

            var response = new RestaurantResponse(
                restaurant.Id,
                restaurant.Title,
                restaurant.Subtitle,
                restaurant.Description,
                restaurant.UserId,
                user,
                restaurant.Address,
                restaurant.Kitchen,
                restaurant.MenuIds,
                restaurant.Photos,
                restaurant.Raiting,
                restaurant.StartWorkTime,
                restaurant.EndWorkTime,
                restaurant.SchemeId
                );

            return Ok(response);
        }

        [HttpGet("User/{id:guid}")]
        public async Task<ActionResult<RestaurantResponse>> GetRestaurantByUserId(Guid id)
        {
            var restaurants = await _restaurantsService.GetByUserId(id);

            var response = restaurants
                .Select(r => new RestaurantResponse(
                    r.Id,
                    r.Title,
                    r.Subtitle,
                    r.Description,
                    r.UserId,
                    new UserResponse(
                        r.User.Id,
                        r.User.Login,
                        r.User.Role,
                        r.User.FirstName,
                        r.User.LastName,
                        r.User.MiddleName,
                        r.User.Phone
                        ),
                    r.Address,
                    r.Kitchen,
                    r.MenuIds,
                    r.Photos,
                    r.Raiting,
                    r.StartWorkTime,
                    r.EndWorkTime,
                    r.SchemeId
                    )
                );

            return Ok(response);
        }

        [HttpPost("Search")]
        public async Task<ActionResult> GetRestaurant([FromBody] RestaruantSearchRequest request)
        {
            var restaurants = await _restaurantsService.GetByFilter(request.title.ToLower(), request.kitchenIds);

            var response = restaurants
                .Select(r => new RestaurantResponse(
                    r.Id,
                    r.Title,
                    r.Subtitle,
                    r.Description,
                    r.UserId,
                    new UserResponse(
                        r.User.Id,
                        r.User.Login,
                        r.User.Role,
                        r.User.FirstName,
                        r.User.LastName,
                        r.User.MiddleName,
                        r.User.Phone
                        ),
                    r.Address,
                    r.Kitchen,
                    r.MenuIds,
                    r.Photos,
                    r.Raiting,
                    r.StartWorkTime,
                    r.EndWorkTime,
                    r.SchemeId
                    ));

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateRestaurant([FromBody] RestaurantRequest request)
        {
            var restaurant = Restaurant.Create(
                Guid.NewGuid(),
                request.title,
                request.subtitle,
                request.description,
                request.userId,
                null,
                request.address,
                [],
                [],
                [],
                0f,
                request.startWorkTime,
                request.endWorkTime,
                Guid.NewGuid()
                );

            if (restaurant.IsFailure) return BadRequest(restaurant.Error);

            var scheme = Scheme.Create(
                Guid.NewGuid(),
                restaurant.Value.Id,
                null,
                []
                );

            restaurant.Value.SchemeId = scheme.Value.Id;
            await _restaurantsService.CreateRestaurant(restaurant.Value);
            await _schemesService.CreateScheme(scheme.Value);

            return Ok(restaurant.Value.Id);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateRestaurant(Guid id, [FromBody] RestaurantRequest request)
        {
            var restaurant = new Restaurant();

            restaurant.Title = request.title;
            restaurant.Subtitle = request.title;
            restaurant.Description = request.description;
            restaurant.StartWorkTime = request.startWorkTime;
            restaurant.EndWorkTime = request.endWorkTime;
            restaurant.Kitchen = request.kitchen;

            await _restaurantsService.UpdateRestaurant(
                id,
                restaurant.Title,
                restaurant.Subtitle,
                restaurant.Description,
                restaurant.Address,
                restaurant.Kitchen,
                restaurant.MenuIds,
                restaurant.Photos,
                restaurant.StartWorkTime,
                restaurant.EndWorkTime
                );

            return Ok(id);
        }
    }
}