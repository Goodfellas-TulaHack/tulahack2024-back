using CSharpFunctionalExtensions;

namespace TulaHack.Core.Models
{
    public class Kitchen
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public Kitchen(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public static Result<Kitchen> Create(Guid id, string name)
        {
            var kitchen = new Kitchen(id, name);

            return Result.Success(kitchen);
        }
    }
}
