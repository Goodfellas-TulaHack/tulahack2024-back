using Microsoft.AspNetCore.Mvc;
using TulaHack.API.Contracts;
using TulaHack.Application.Services;
using TulaHack.Core.Models;

namespace TulaHack.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationsService _notificationsService;

        public NotificationController(NotificationsService notificationsService)
        {
            _notificationsService = notificationsService;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<List<Notification>>> GetNotificationsByResourantId(Guid id)
        {
            var notifications = await _notificationsService.GetNotificationsByRestourantId(id);

            var response = notifications
                .Select(n => new NotificationResponse(
                    n.Id,
                    n.UserId,
                    new UserResponse(
                        n.User.Id,
                        n.User.Login,
                        n.User.Role,
                        n.User.FirstName,
                        n.User.LastName,
                        n.User.MiddleName,
                        n.User.Phone
                        ),
                    n.RestaurantId,
                    new RestaurantResponse(
                        n.Restaurant.Id,
                        n.Restaurant.Title,
                        n.Restaurant.Subtitle,
                        n.Restaurant.Description,
                        n.Restaurant.UserId,
                        null,
                        n.Restaurant.Address,
                        n.Restaurant.Kitchen,
                        n.Restaurant.MenuIds,
                        n.Restaurant.Photos,
                        n.Restaurant.Raiting,
                        n.Restaurant.StartWorkTime,
                        n.Restaurant.EndWorkTime,
                        n.Restaurant.SchemeId
                        ),
                    n.Type,
                    n.Description
                    )).ToList();


            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateNotification([FromBody] NotificationRequest request)
        {
            var notification = Notification.Create(
                Guid.NewGuid(),
                request.userId,
                null,
                request.restaurantId,
                null,
                request.type,
                request.description
                );

            if (notification.IsFailure) return BadRequest(notification.Error);

            var response = await _notificationsService.CreateNotification(notification.Value);

            return Ok(response);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid?>> UpdateNotification(Guid id, [FromBody] NotificationRequest request)
        {
            return Ok(await _notificationsService.UpdateNotification(id, request.type));
        }
    }
}
