namespace TulaHack.API.Contracts
{
    public record RestaurantRequest(
        string Title,
        string Subtitle,
        string Description,
        Guid UserId,
        string Address,
        List<Guid> Kitchen,
        List<Guid> MenuIds,
        List<string> Photos,
        string StartWorkTime,
        string EndWorkTime,
        Guid SchemeId
        );
}
