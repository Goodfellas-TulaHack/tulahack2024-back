using Microsoft.AspNetCore.Mvc;
using TulaHack.API.Contracts;
using TulaHack.Application.Services;

namespace TulaHack.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly BookingService _bookingService;

        public BookingController(BookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet("{id=guid}")]
        public async Task<ActionResult<BookingResponse>> GetBooking(Guid id)
        {
            var booking = await _bookingService.GetBooking(id);

            if (booking == null) return BadRequest("Booking not found");

            var response = new BookingResponse(
                booking.Id,
                booking.UserId,
                new UserResponse(
                    booking.User.Id,
                    booking.User.Login,
                    booking.User.Role,
                    booking.User.FirstName,
                    booking.User.LastName,
                    booking.User.MiddleName,
                    booking.User.Phone
                    ),
                booking.RestaurantId,
                new RestaurantResponse(
                    booking.Restaurant.Id,
                    booking.Restaurant.Title,
                    booking.Restaurant.Subtitle,
                    booking.Restaurant.Description,
                    booking.Restaurant.UserId,
                    null,
                    booking.Restaurant.Address,
                    booking.Restaurant.Kitchen,
                    booking.Restaurant.MenuIds,
                    booking.Restaurant.Photos,
                    booking.Restaurant.Raiting,
                    booking.Restaurant.StartWorkTime,
                    booking.Restaurant.EndWorkTime,
                    booking.Restaurant.SchemeId
                    ),
                booking.TableId,
                booking.Date,
                booking.StartTime,
                booking.EndTime,
                booking.PersonsNumber,
                booking.Status,
                true
                );

            return Ok(response);
        }

        [HttpGet("Restaurant/{id=guid}")]
        public async Task<ActionResult<BookingResponse>> GetBookingsByRestaurantId(Guid id)
        {
            var bookings = await _bookingService.GetBookingsByRestaurantId(id);

            var response = new List<BookingResponse>();
            var slotTime = TimeOnly.Parse(bookings[0].Restaurant.StartWorkTime);
            var i = 0;
            while (slotTime < TimeOnly.Parse(bookings[0].Restaurant.EndWorkTime))
            {
                var isReserved = false;
                foreach (var booking in bookings)
                {
                    if (slotTime >= TimeOnly.Parse(booking.StartTime) && slotTime <= TimeOnly.Parse(booking.EndTime))
                    {
                        isReserved = true;

                        response.Add(
                            new BookingResponse(
                                booking.Id,
                                booking.UserId,
                                new UserResponse(
                                    booking.User.Id,
                                    booking.User.Login,
                                    booking.User.Role,
                                    booking.User.FirstName,
                                    booking.User.LastName,
                                    booking.User.MiddleName,
                                    booking.User.Phone
                                    ),
                                booking.RestaurantId,
                                new RestaurantResponse(
                                    booking.Restaurant.Id,
                                    booking.Restaurant.Title,
                                    booking.Restaurant.Subtitle,
                                    booking.Restaurant.Description,
                                    booking.Restaurant.UserId,
                                    null,
                                    booking.Restaurant.Address,
                                    booking.Restaurant.Kitchen,
                                    booking.Restaurant.MenuIds,
                                    booking.Restaurant.Photos,
                                    booking.Restaurant.Raiting,
                                    booking.Restaurant.StartWorkTime,
                                    booking.Restaurant.EndWorkTime,
                                    booking.Restaurant.SchemeId
                                    ),
                                booking.TableId,
                                booking.Date,
                                booking.StartTime,
                                booking.EndTime,
                                booking.PersonsNumber,
                                booking.Status,
                                isReserved
                            ));

                        break;
                    }
                }

                if (!isReserved)
                {
                    response.Add(
                        new BookingResponse(
                            default,
                            default,
                            default,
                            default,
                            default,
                            default,
                            default,
                            default,
                            default,
                            default,
                            default,
                            false
                        ));
                } 

                slotTime = slotTime.AddMinutes(15);
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateBooking([FromBody] BookingRequest request)
        {
            var booking = Core.Models.Booking.Create(
                Guid.NewGuid(),
                request.UserId,
                null,
                request.RestaurantId,
                null,
                request.TableId,
                request.Date,
                request.StartTime,
                request.EndTime,
                request.PersonsNumber,
                0
                );

            if (booking.IsFailure) return BadRequest(booking.Error);

            var bookings = await _bookingService.GetBookingsByRestaurantId(booking.Value.RestaurantId);

            foreach (var bookingItem in bookings)
            {
                if (TimeOnly.Parse(booking.Value.StartTime) >= TimeOnly.Parse(bookingItem.StartTime) && TimeOnly.Parse(booking.Value.StartTime) <= TimeOnly.Parse(booking.Value.EndTime))
                {
                    return BadRequest("Time is taked");
                }
            }

            await _bookingService.CreateBooking(booking.Value);

            return Ok(booking.Value.Id);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateBooking(Guid id, [FromBody] BookingRequest request)
        {
            var booking = Core.Models.Booking.Create(
                id,
                request.UserId,
                null,
                request.RestaurantId,
                null,
                request.TableId,
                request.Date,
                request.StartTime,
                request.EndTime,
                request.PersonsNumber,
                request.status
                );

            await _bookingService.UpdateBooking(
                booking.Value.Id,
                booking.Value.Status);

            return Ok(booking.Value.Id);
        }
    }
}
