using TulaHack.Core.Models;
using TulaHack.DataAccess.Repositories;

namespace TulaHack.Application.Services
{
    public class NotificationsService
    {
        private readonly NotificationsRepository _notificationsRepository;

        public NotificationsService(NotificationsRepository notificationsRepository)
        {
            _notificationsRepository = notificationsRepository;
        }

        public async Task<List<Notification>> GetNotificationsByRestourantId(Guid id)
        {
            return await _notificationsRepository.GetByRestaurantId(id);
        }

        public async Task<Notification> GetNotificationById(Guid id)
        {
            return await _notificationsRepository.GetById(id);
        }

        public async Task<Guid> CreateNotification(Notification notification)
        {
            return await _notificationsRepository.Create(notification);
        }

        public async Task<Guid> UpdateNotification(Guid id, string status)
        {
            return await _notificationsRepository.Update(id, status);
        }
    }
}
