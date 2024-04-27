using TulaHack.Core.Models;

namespace TulaHack.Core.Abstractions
{
    public interface IUsersRepository
    {
        Task<Guid> Create(User user);
        Task<User?> GetById(Guid id);
        Task<User?> GetByLogin(string login);
        Task<Guid?> Update(Guid id, string firstName, string lastName, string middleName, string phone);
    }
}