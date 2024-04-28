namespace TulaHack.DataAccess.Models
{
    public class MenuEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid RestaurantId { get; set; }
        public RestaurantEntity? Restaurant { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
    }
}
