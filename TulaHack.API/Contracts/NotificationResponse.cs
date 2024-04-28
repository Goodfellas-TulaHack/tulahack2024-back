namespace TulaHack.API.Contracts
{
    public record NotificationResponse(
        Guid id,
        Guid userId,
        UserResponse user,
        Guid restaurantId,
        RestaurantResponse restaurant,
        string type,
        string description
        );
}
