using TulaHack.Core.Models;

namespace TulaHack.Core.Abstractions
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}