namespace TulaHack.DataAccess.Models
{
    public class TableEntity
    {
        public Guid Id { get; set; }
        public Guid SchemeId { get; set; }
        public SchemeEntity? Scheme { get; set; }
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
    }
}
