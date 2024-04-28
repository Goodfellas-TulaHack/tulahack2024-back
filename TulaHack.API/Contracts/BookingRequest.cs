namespace TulaHack.API.Contracts
{
    public record BookingRequest(
        Guid userId,
        Guid restaurantId,
        Guid tableId,
        string date,
        string startTime,
        string endTime,
        int personsNumber,
        int status
        );
}
