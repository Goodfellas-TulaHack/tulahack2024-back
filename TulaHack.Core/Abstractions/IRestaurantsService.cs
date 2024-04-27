using TulaHack.Core.Models;

namespace TulaHack.Core.Abstractions
{
    public interface IRestaurantsService
    {
        Task<Guid> CreateRestaurant(Restaurant restaurant);
        Task<List<Restaurant>> Get();
        Task<Restaurant?> GetById(Guid id);
        Task<Guid?> UpdateRestaurant(Guid id, string title, string subtitle, string description, string address, List<Guid> kitchen, List<Guid> menuIds, List<string> photos, string startWorkTime, string endWorkTime);
    }
}