using CSharpFunctionalExtensions;

namespace TulaHack.Core.Models
{
    public class Scheme
    {
        public Guid Id { get; set; }
        public Guid RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; }
        public List<Guid> TableIds { get; set; } = [];

        public Scheme(Guid id, Guid restaurantId, Restaurant? restaurant, List<Guid> tableIds)
        {
            Id = id;
            RestaurantId = restaurantId;
            Restaurant = restaurant;
            TableIds = tableIds;
        }

        public static Result<Scheme> Create(Guid id, Guid restaurantId, Restaurant? restaurant, List<Guid> tableIds)
        {
            return Result.Success(new Scheme (
                id,
                restaurantId,
                restaurant,
                tableIds
                )
            );
        }
    }
}
