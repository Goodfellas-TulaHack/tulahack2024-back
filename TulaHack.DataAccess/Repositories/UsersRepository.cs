using Microsoft.EntityFrameworkCore;
using TulaHack.Core.Models;
using TulaHack.DataAccess.Models;

namespace TulaHack.DataAccess.Repositories
{
    public class UsersRepository
    {
        private readonly TulaHackDbContext _dbContext;

        public UsersRepository(TulaHackDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> GetById(Guid id)
        {
            var userEntity = await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            if (userEntity == null) return null;

            var user = User.Create(userEntity.Id, userEntity.Login, userEntity.Password, userEntity.Role,
                userEntity.FirstName, userEntity.LastName, userEntity.MiddleName, userEntity.Phone);

            return user.Value;
        }

        public async Task<User?> GetByLogin(string login)
        {
            var userEntity = await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Login == login);

            if (userEntity == null) return null;

            var user = User.Create(userEntity.Id, userEntity.Login, userEntity.Password, userEntity.Role,
                userEntity.FirstName, userEntity.LastName, userEntity.MiddleName, userEntity.Phone);

            return user.Value;
        }

        public async Task<Guid> Create(User user)
        {
            var userEntity = new UserEntity
            {
                Id = user.Id,
                Login = user.Login,
                Role = user.Role,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MiddleName = user.MiddleName,
                Phone = user.Phone,
            };

            await _dbContext.AddAsync(userEntity);
            await _dbContext.SaveChangesAsync();

            return user.Id;
        }

        public async Task<Guid?> Update(Guid id, string firstName, string lastName, string middleName, string phone)
        {
            var userEntity = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (userEntity == null) return null;

            userEntity.FirstName = firstName;
            userEntity.LastName = lastName;
            userEntity.MiddleName = middleName;
            userEntity.Phone = phone;

            await _dbContext.SaveChangesAsync();

            return id;
        }
    }
}
