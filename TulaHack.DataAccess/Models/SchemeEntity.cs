using TulaHack.Core.Models;

namespace TulaHack.DataAccess.Models
{
    public class SchemeEntity
    {
        public Guid Id { get; set; }
        public Guid RestaurantId { get; set; }
        public RestaurantEntity? Restaurant { get; set; }
        public List<Guid> TableIds { get; set; } = [];
    }
}
