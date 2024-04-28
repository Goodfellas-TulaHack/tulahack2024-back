using CSharpFunctionalExtensions;
using System.IdentityModel.Tokens.Jwt;
using TulaHack.Application.Authentification;
using TulaHack.Core.Abstractions;
using TulaHack.Core.Models;
using TulaHack.DataAccess.Repositories;

namespace TulaHack.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly UsersRepository _usersRepository;
        private readonly PasswordHasher _passwordHasher;
        private readonly JwtProvider _jwtProvider;

        public UsersService(UsersRepository usersRepository, PasswordHasher passwordHasher, JwtProvider jwtProvider)
        {
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }

        public async Task<User?> GetById(Guid id)
        {
            return await _usersRepository.GetById(id);
        }

        public async Task<User?> GetByLogin(string login)
        {
            return await _usersRepository.GetByLogin(login);
        }

        public async Task<Guid> RegisterUser(User user)
        {
            return await _usersRepository.Create(user);
        }

        public async Task<Guid?> UpdateUser(Guid id, string firstName, string lastName, string middleName, string phone)
        {
            return await _usersRepository.Update(id, firstName, lastName, middleName, phone);
        }

        public async Task<Result<string>> LoginUser(string login, string password)
        {
            var user = await _usersRepository.GetByLogin(login);

            if (user == null)
            {
                return Result.Failure<string>("Invalid username or password");
            }

            if (!_passwordHasher.Verify(password, user.Password))
            {
                return Result.Failure<string>("Invalid username or password");
            }

            return _jwtProvider.GenerateToken(user);
        }

        public async Task<bool> AuthUser(string token)
        {
            return _jwtProvider.ValidateToken(token);
        }
    }
}
