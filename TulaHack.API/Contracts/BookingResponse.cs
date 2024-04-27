namespace TulaHack.API.Contracts
{
    public record BookingResponse(
        Guid Id,
        Guid UserId,
        UserResponse User,
        Guid RestaurantId,
        RestaurantResponse Restaurant,
        Guid TableId,
        string Date,
        string StartTime,
        string EndTime,
        int PersonsNumber,
        int Status,
        bool IsReserved
        );
}
