namespace TulaHack.API.Contracts
{
    public record UserRequest(
        string Login,
        string Password,
        int Role,
        string FirstName,
        string LastName,
        string MiddleName,
        string Phone
        );
}
