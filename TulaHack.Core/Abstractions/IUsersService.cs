using TulaHack.Core.Models;

namespace TulaHack.Core.Abstractions
{
    public interface IUsersService
    {
        Task<Guid> RegisterUser(User user);
        Task<User?> GetById(Guid id);
        Task<Guid?> UpdateUser(Guid id, string firstName, string lastName, string middleName, string phone);
    }
}