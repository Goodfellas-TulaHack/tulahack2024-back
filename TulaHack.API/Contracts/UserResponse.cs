namespace TulaHack.API.Contracts
{
    public record UserResponse(
        Guid id,
        string login,
        int role,
        string firstName,
        string lastName,
        string middleName,
        string phone
        );
}
