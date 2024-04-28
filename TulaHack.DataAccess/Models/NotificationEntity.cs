using TulaHack.Core.Models;

namespace TulaHack.DataAccess.Models
{
    public class NotificationEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public UserEntity? User { get; set; }
        public Guid RestaurantId { get; set; }
        public RestaurantEntity? Restaurant { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
