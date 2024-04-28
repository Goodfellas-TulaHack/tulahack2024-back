namespace TulaHack.API.Contracts
{
    public record MenuResponse(
        Guid id,
        string name,
        Guid resourantId,
        RestaurantResponse restourant,
        decimal price,
        string description,
        string photo
        );
}
