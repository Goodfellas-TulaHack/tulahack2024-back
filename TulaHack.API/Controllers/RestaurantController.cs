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

        public RestaurantController(RestaurantsService restaurantsService)
        {
            _restaurantsService = restaurantsService;
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

        [HttpGet("{id=guid}")]
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

        [HttpGet("User/{id=guid}")]
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
        public async Task<IActionResult> GetRestaurant([FromBody] RestaruantSearchRequest request)
        {
            var restaurants = await _restaurantsService.GetByFilter(request.Title.ToLower(), request.Kitchen.ToLower());

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
                request.Title,
                request.Subtitle,
                request.Description,
                request.UserId,
                null,
                request.Address,
                request.Kitchen,
                request.MenuIds,
                request.Photos,
                0f,
                request.StartWorkTime,
                request.EndWorkTime,
                request.SchemeId
                );

            if (restaurant.IsFailure) return BadRequest(restaurant.Error);

            await _restaurantsService.CreateRestaurant(restaurant.Value);

            return Ok(restaurant.Value.Id);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateRestaurant(Guid id, [FromBody] RestaurantRequest request)
        {
            var restaurant = Restaurant.Create(
                Guid.NewGuid(),
                request.Title,
                request.Subtitle,
                request.Description,
                request.UserId,
                null,
                request.Address,
                request.Kitchen,
                request.MenuIds,
                request.Photos,
                0f,
                request.StartWorkTime,
                request.EndWorkTime,
                request.SchemeId
                );

            if (restaurant.IsFailure) return BadRequest(restaurant.Error);

            await _restaurantsService.UpdateRestaurant(
                restaurant.Value.Id,
                restaurant.Value.Title,
                restaurant.Value.Subtitle,
                restaurant.Value.Description,
                restaurant.Value.Address,
                restaurant.Value.Kitchen,
                restaurant.Value.MenuIds,
                restaurant.Value.Photos,
                restaurant.Value.StartWorkTime,
                restaurant.Value.EndWorkTime
                );

            return Ok(restaurant.Value.Id);
        }
    }
}