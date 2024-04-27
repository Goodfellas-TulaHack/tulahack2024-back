namespace TulaHack.API.Contracts
{
    public record BookingRequest(
        Guid UserId,
        Guid RestaurantId,
        Guid TableId,
        string Date,
        string StartTime,
        string EndTime,
        int PersonsNumber,
        int status
        );
}
