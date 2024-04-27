using CSharpFunctionalExtensions;

namespace TulaHack.Core.Models
{
    public class User
    {
        public Guid Id { get; }
        public string Login { get; } = string.Empty;
        public string Password { get; } = string.Empty;
        public int Role { get; } = 0;
        public string FirstName { get; } = string.Empty;
        public string LastName { get; } = string.Empty;
        public string MiddleName { get; } = string.Empty;
        public string Phone { get; } = string.Empty;


        public const int MAX_STRING_LENGTH = 50;
        public const int MIN_PASSWORD_LENGTH = 6;


        private User(Guid id, string login, string password, int role, string firstName, string lastName, string middleName, string phone)
        {
            Id = id;
            Login = login;
            Password = password;
            Role = role;
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Phone = phone;
        }

        public static Result<User> Create(Guid id, string login, string password, int role, string firstName, string lastName, string middleName, string phone)
        {
            if (string.IsNullOrWhiteSpace(login) || login.Length > MAX_STRING_LENGTH)
            {
                return Result.Failure<User>($"'{nameof(login)}' connot be empty or > {MAX_STRING_LENGTH}");
            }

            if (string.IsNullOrWhiteSpace(firstName) || firstName.Length > MAX_STRING_LENGTH)
            {
                return Result.Failure<User>($"'{nameof(firstName)}' connot be empty or > {MAX_STRING_LENGTH}");
            }

            if (string.IsNullOrWhiteSpace(lastName) || lastName.Length > MAX_STRING_LENGTH)
            {
                return Result.Failure<User>($"'{nameof(firstName)}' connot be empty or > {MAX_STRING_LENGTH}");
            }

            if (string.IsNullOrWhiteSpace(middleName) || middleName.Length > MAX_STRING_LENGTH)
            {
                return Result.Failure<User>($"'{nameof(middleName)}' connot be empty or > {MAX_STRING_LENGTH}");
            }

/*            if (password.Length < MIN_PASSWORD_LENGTH)
            {
                return Result.Failure<User>($"'{nameof(password)}' connot be empty or < {MIN_PASSWORD_LENGTH}");
            }*/

            var user = new User(id, login, password, role, firstName, lastName, middleName, phone);

            return Result.Success(user);
        }
    }
}
