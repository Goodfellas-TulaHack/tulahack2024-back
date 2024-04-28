namespace TulaHack.API.Contracts
{
    public record RestaurantResponse(
        Guid id,
        string title,
        string subtitle,
        string description,
        Guid userId,
        UserResponse user,
        string address,
        List<Guid> kitchen,
        List<Guid> menuIds,
        List<string> photos,
        float raiting,
        string startWorkTime,
        string endWorkTime,
        Guid schemeId
        );
}
