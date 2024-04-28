using Microsoft.EntityFrameworkCore;
using TulaHack.Core.Models;
using TulaHack.DataAccess.Models;

namespace TulaHack.DataAccess.Repositories
{
    public class NotificationsRepository
    {
        private readonly TulaHackDbContext _dbContext;
        public NotificationsRepository(TulaHackDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Notification> GetById(Guid id)
        {
            var notificationEntity = await _dbContext.Notifications
                .AsNoTracking()
                .Include(b => b.User)
                .Include(b => b.Restaurant)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (notificationEntity == null) return null;

            var notification = Notification.Create(
                    notificationEntity.Id,
                    notificationEntity.UserId,
                    User.Create(
                        notificationEntity.User.Id,
                        notificationEntity.User.Login,
                        string.Empty,
                        notificationEntity.User.Role,
                        notificationEntity.User.FirstName,
                        notificationEntity.User.LastName,
                        notificationEntity.User.MiddleName,
                        notificationEntity.User.Phone
                        ).Value,
                    notificationEntity.RestaurantId,
                    Restaurant.Create(
                        notificationEntity.Restaurant.Id,
                        notificationEntity.Restaurant.Title,
                        notificationEntity.Restaurant.Subtitle,
                        notificationEntity.Restaurant.Description,
                        notificationEntity.Restaurant.UserId,
                        null,
                        notificationEntity.Restaurant.Address,
                        notificationEntity.Restaurant.Kitchens,
                        notificationEntity.Restaurant.MenuIds,
                        notificationEntity.Restaurant.Photos,
                        notificationEntity.Restaurant.Raiting,
                        notificationEntity.Restaurant.StartWorkTime,
                        notificationEntity.Restaurant.EndWorkTime,
                        notificationEntity.Restaurant.SchemeId
                        ).Value,
                    notificationEntity.Type,
                    notificationEntity.Description
                    );

            return notification.Value;
        }

        public async Task<List<Notification>> GetByRestaurantId(Guid id)
        {
            var notificationEntities = await _dbContext.Notifications
                .AsNoTracking()
                .Include(b => b.User)
                .Include(b => b.Restaurant)
                .Where(n => n.RestaurantId == id)
                .ToListAsync();

            var notifications = notificationEntities
                .Select(n => Notification.Create(
                    n.Id,
                    n.UserId,
                    User.Create(
                        n.User.Id,
                        n.User.Login,
                        string.Empty,
                        n.User.Role,
                        n.User.FirstName,
                        n.User.LastName,
                        n.User.MiddleName,
                        n.User.Phone
                        ).Value,
                    n.RestaurantId,
                    Restaurant.Create(
                        n.Restaurant.Id,
                        n.Restaurant.Title,
                        n.Restaurant.Subtitle,
                        n.Restaurant.Description,
                        n.Restaurant.UserId,
                        null,
                        n.Restaurant.Address,
                        n.Restaurant.Kitchens,
                        n.Restaurant.MenuIds,
                        n.Restaurant.Photos,
                        n.Restaurant.Raiting,
                        n.Restaurant.StartWorkTime,
                        n.Restaurant.EndWorkTime,
                        n.Restaurant.SchemeId
                        ).Value,
                    n.Type,
                    n.Description
                    ).Value
                ).ToList();

            return notifications;
        }

        public async Task<Guid> Create(Notification notification)
        {
            var notificationEntity = new NotificationEntity
            {
                Id = notification.Id,
                UserId = notification.UserId,
                RestaurantId = notification.RestaurantId,
                Type = notification.Type,
                Description = notification.Description
            };

            await _dbContext.AddAsync(notificationEntity);
            await _dbContext.SaveChangesAsync();

            return notification.Id;
        }

        public async Task<Guid> Update(Guid id, string type)
        {
            var notificationEntity = await _dbContext.Notifications.FirstOrDefaultAsync(n => n.Id == id);

            notificationEntity.Type = type;

            await _dbContext.SaveChangesAsync();

            return id;
        }
    }
}
