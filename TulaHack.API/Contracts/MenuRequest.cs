namespace TulaHack.API.Contracts
{
    public record MenuRequest(
        string name,
        Guid resourantId,
        decimal price,
        string description,
        string photo
        );
}
