using TulaHack.Core.Models;

namespace TulaHack.API.Contracts
{
    public record SchemeResponse(
        Guid id,
        Guid restaurantId,
        RestaurantResponse restaurant,
        List<Guid> tableIds
        );
}