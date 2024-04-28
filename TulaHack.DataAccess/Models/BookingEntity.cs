namespace TulaHack.DataAccess.Models
{
    public class BookingEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public UserEntity? User { get; set; }
        public Guid RestaurantId { get; set; }
        public RestaurantEntity? Restaurant { get; set; }
        public Guid TableId { get; set; }
        public string Date { get; set; } = string.Empty;
        public string StartTime { get; set; } = string.Empty;
        public string EndTime { get; set; } = string.Empty;
        public int PersonsNumber { get; set; }
        public int Status { get; set; }
    }
}
