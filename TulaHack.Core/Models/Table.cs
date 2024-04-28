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
        public float ScaleX { get; set; }
        public float ScaleY { get; set; }
        public float Rotate { get; set; }
        public float Radius { get; set; }
        public string Fill { get; set; }
        public string Type { get; set; }


        private Table(Guid id, Guid schemeId, Scheme? scheme, int persons, float x, float y, 
            float width, float height, float scaleX, float scaleY, float rotate, float radius,
            string fill, string type)
        {
            Id = id;
            SchemeId = schemeId;
            Scheme = scheme;
            Persons = persons;
            X = x;
            Y = y;
            Width = width;
            Height = height;
            ScaleX = scaleX;
            ScaleY = scaleY;
            Rotate = rotate;
            Radius = radius;
            Fill = fill;
            Type = type;
        }

        public static Result<Table> Create(Guid id, Guid schemeId, Scheme? scheme, int persons, 
            float x, float y, float width, float height, float scaleX, float scaleY, float rotate,
            float radius, string fill, string type)
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
                height,
                scaleX,
                scaleY,
                rotate,
                radius,
                fill,
                type
                );

            return Result.Success(table);
        }
    }
}
