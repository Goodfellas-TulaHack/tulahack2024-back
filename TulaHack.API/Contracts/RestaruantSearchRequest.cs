namespace TulaHack.API.Contracts
{
    public record RestaruantSearchRequest(
        string title,
        List<Guid> kitchenIds
        );
}
