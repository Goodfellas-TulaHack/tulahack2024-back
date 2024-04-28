namespace TulaHack.API.Contracts
{
    public record BookingResponse(
        Guid id,
        Guid userId,
        UserResponse user,
        Guid restaurantId,
        RestaurantResponse restaurant,
        Guid tableId,
        string date,
        string startTime,
        string endTime,
        int personsNumber,
        int status,
        bool isReserved
        );
}
