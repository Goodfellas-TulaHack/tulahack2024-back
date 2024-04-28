using CSharpFunctionalExtensions;

namespace TulaHack.Core.Models
{
    public class Notification
    {
        public const int MAX_STRING_LENGTH = 100;

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public Guid RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;


        public Notification() { }
        private Notification(Guid id, Guid userId, User? user, Guid restaurantId, 
            Restaurant? restaurant, string type, string description)
        {
            Id = id;
            UserId = userId;
            User = user;
            RestaurantId = restaurantId;
            Restaurant = restaurant;
            Type = type;
            Description = description;
        }

        public static Result<Notification> Create(Guid id, Guid userId, User? user, 
            Guid restaurantId, Restaurant? restaurant, string type, string description)
        {
            if (string.IsNullOrWhiteSpace(description) || description.Length > MAX_STRING_LENGTH)
            {
                return Result.Failure<Notification>($"'{nameof(description)}' length must be < {MAX_STRING_LENGTH}");
            }

            var notification = new Notification(id, userId, user, restaurantId, restaurant, type, description);

            return Result.Success(notification);
        }

    }
}
