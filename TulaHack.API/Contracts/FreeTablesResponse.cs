namespace TulaHack.API.Contracts
{
    public record FreeTablesResponse(
        List<Guid> tableId,
        string date,
        string time
        );
}
