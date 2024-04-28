namespace TulaHack.API.Contracts
{
    public record RestaurantRequest(
        string title,
        string subtitle,
        string description,
        Guid userId,
        string address,
        string startWorkTime,
        string endWorkTime,
        List<Guid> kitchen
        );
}
