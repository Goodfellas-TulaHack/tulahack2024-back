namespace TulaHack.API.Contracts
{
    public record RestaurantRequest(
        string Title,
        string Subtitle,
        string Description,
        Guid UserId,
        string Address,
        string Kitchen,
        List<Guid> MenuIds,
        List<string> Photos,
        string StartWorkTime,
        string EndWorkTime,
        Guid SchemeId
        );
}
