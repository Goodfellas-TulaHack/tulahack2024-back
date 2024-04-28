namespace TulaHack.API.Contracts
{
    public record UserLoginRequest(
        string login,
        string password);
}
