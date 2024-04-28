using TulaHack.Core.Abstractions;
using TulaHack.Core.Models;
using TulaHack.DataAccess.Repositories;

namespace TulaHack.Application.Services
{
    public class RestaurantsService
    {
        private readonly RestaurantsRepository _usersRepository;

        public RestaurantsService(RestaurantsRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<List<Restaurant>> Get()
        {
            return await _usersRepository.Get();
        }

        public async Task<Restaurant?> GetById(Guid id)
        {
            return await _usersRepository.GetById(id);
        }

        public async Task<List<Restaurant>> GetByFilter(string title, List<Guid> kitchenIds)
        {
            return await _usersRepository.GetByFilter(title, kitchenIds);
        }

        public async Task<List<Restaurant>> GetByUserId(Guid id)
        {
            return await _usersRepository.GetByUserId(id);
        }

        public async Task<Guid> CreateRestaurant(Restaurant restaurant)
        {
            return await _usersRepository.Create(restaurant);
        }

        public async Task<Guid?> UpdateRestaurant(Guid id, string title, string subtitle, string description, string address, List<Guid> kitchen,
            List<Guid> menuIds, List<string> photos, string startWorkTime, string endWorkTime)
        {
            return await _usersRepository.Update(id, title, subtitle, description, address, kitchen, menuIds, photos, startWorkTime, endWorkTime);
        }
    }
}
