namespace TulaHack.API.Contracts
{
    public record UserRequest(
        string login,
        string password,
        int role,
        string firstName,
        string lastName,
        string middleName,
        string phone
        );
}
