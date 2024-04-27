namespace TulaHack.API.Contracts
{
    public record UserResponse(
        Guid Id,
        string Login,
        int Role,
        string FirstName,
        string LastName,
        string MiddleName,
        string Phone
        );
}
