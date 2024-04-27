using CSharpFunctionalExtensions;

namespace TulaHack.Core.Models
{
    public class Table
    {
        public Guid Id { get; set; }
        public Guid SchemeId { get; set; }
        public Scheme? Scheme { get; set; }
        public int Persons { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public Table(Guid id, Guid schemeId, Scheme? scheme, int persons, float x, float y, 
            float width, float height)
        {
            Id = id;
            SchemeId = schemeId;
            Scheme = scheme;
            Persons = persons;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public static Result<Table> Create(Guid id, Guid schemeId, Scheme? scheme, int persons, 
            float x, float y, float width, float height)
        {
            if (persons < 0)
            {
                return Result.Failure<Table>($"'{nameof(persons)}' cannot be < 0");
            }

            var table = new Table(
                id,
                schemeId,
                scheme,
                persons,
                x,
                y,
                width,
                height
                );

            return Result.Success(table);
        }
    }
}
