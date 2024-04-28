namespace TulaHack.DataAccess.Models
{
    public class RestaurantEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Subtitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public UserEntity? User { get; set; } = null;
        public string Address { get; set; } = string.Empty;
        public List<Guid> Kitchens { get; set; } = [];
        public List<Guid> MenuIds { get; set; } = [];
        public string Logo { get; set; } = string.Empty;
        public List<string> Photos { get; set; } = [];
        public float Raiting { get; set; } = 0f;
        public string StartWorkTime { get; set; } = string.Empty;
        public string EndWorkTime { get; set; } = string.Empty;
        public Guid SchemeId { get; set; }
    }
}
