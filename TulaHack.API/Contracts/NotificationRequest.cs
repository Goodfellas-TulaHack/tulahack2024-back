namespace TulaHack.API.Contracts
{
    public record NotificationRequest(
        Guid userId,
        Guid restaurantId,
        string type,
        string description
        );
}
