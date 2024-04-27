namespace TulaHack.API.Contracts
{
    public record RestaurantResponse(
        Guid Id,
        string Title,
        string Subtitle,
        string Description,
        Guid UserId,
        UserResponse user,
        string Address,
        string Kitchen,
        List<Guid> MenuIds,
        List<string> Photos,
        float Raiting,
        string StartWorkTime,
        string EndWorkTime,
        Guid SchemeId
        );
}
