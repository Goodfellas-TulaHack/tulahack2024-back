using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using TulaHack.Core.Models;
using TulaHack.DataAccess.Models;

namespace TulaHack.DataAccess.Repositories
{
    public class RestaurantsRepository
    {
        private readonly TulaHackDbContext _dbContext;

        public RestaurantsRepository(TulaHackDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Restaurant>> Get()
        {
            var restaurantEntities = await _dbContext.Restaurants
                .AsNoTracking()
                .Include(r => r.User)
                .ToListAsync();

            var restaurants = restaurantEntities
                .Select(r => Restaurant.Create(
                    r.Id,
                    r.Title,
                    r.Subtitle,
                    r.Description,
                    r.UserId,
                    User.Create(
                        r.User.Id,
                        r.User.Login,
                        string.Empty,
                        r.User.Role,
                        r.User.FirstName,
                        r.User.LastName,
                        r.User.MiddleName,
                        r.User.Phone
                        ).Value,
                    r.Address,
                    r.Kitchen,
                    r.MenuIds,
                    r.Photos,
                    r.Raiting,
                    r.StartWorkTime,
                    r.EndWorkTime,
                    r.SchemeId).Value)
                .ToList();

            return restaurants;
        }

        public async Task<Restaurant?> GetById(Guid id)
        {
            var restaurantEntity = await _dbContext.Restaurants
                .AsNoTracking()
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (restaurantEntity == null) return null;

            var restaurant = Restaurant.Create(
                    restaurantEntity.Id,
                    restaurantEntity.Title,
                    restaurantEntity.Subtitle,
                    restaurantEntity.Description,
                    restaurantEntity.UserId,
                    User.Create(
                        restaurantEntity.User.Id,
                        restaurantEntity.User.Login,
                        string.Empty,
                        restaurantEntity.User.Role,
                        restaurantEntity.User.FirstName,
                        restaurantEntity.User.LastName,
                        restaurantEntity.User.MiddleName,
                        restaurantEntity.User.Phone
                        ).Value,
                    restaurantEntity.Address,
                    restaurantEntity.Kitchen,
                    restaurantEntity.MenuIds,
                    restaurantEntity.Photos,
                    restaurantEntity.Raiting,
                    restaurantEntity.StartWorkTime,
                    restaurantEntity.EndWorkTime,
                    restaurantEntity.SchemeId).Value;

            return restaurant;
        }

        public async Task<List<Restaurant>> GetByUserId(Guid id)
        {
            var restaurantEntities = await _dbContext.Restaurants
                .AsNoTracking()
                .Include(r => r.User)
                .Where(r => r.UserId == id)
                .ToListAsync();

            var restaurants = restaurantEntities
                .Select(r => Restaurant.Create(
                    r.Id,
                    r.Title,
                    r.Subtitle,
                    r.Description,
                    r.UserId,
                    User.Create(
                        r.User.Id,
                        r.User.Login,
                        string.Empty,
                        r.User.Role,
                        r.User.FirstName,
                        r.User.LastName,
                        r.User.MiddleName,
                        r.User.Phone
                        ).Value,
                    r.Address,
                    r.Kitchen,
                    r.MenuIds,
                    r.Photos,
                    r.Raiting,
                    r.StartWorkTime,
                    r.EndWorkTime,
                    r.SchemeId).Value)
                .ToList();

            return restaurants;
        }

        public async Task<List<Restaurant>> GetByFilter(string title, string kitchen)
        {
            var query = _dbContext.Restaurants.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
            {
                query = query.Where(r => r.Title.ToLower().Contains(title));
            }

            if (!string.IsNullOrWhiteSpace(kitchen))
            {
                query = query.Where(r => r.Kitchen.ToLower().Contains(kitchen));
            }

            var restaurantEntities = await query
                .Include(r => r.User)
                .ToListAsync();

            var restaurants = restaurantEntities
                .Select(r => Restaurant.Create(
                    r.Id,
                    r.Title,
                    r.Subtitle,
                    r.Description,
                    r.UserId,
                    User.Create(
                        r.User.Id,
                        r.User.Login,
                        string.Empty,
                        r.User.Role,
                        r.User.FirstName,
                        r.User.LastName,
                        r.User.MiddleName,
                        r.User.Phone
                        ).Value,
                    r.Address,
                    r.Kitchen,
                    r.MenuIds,
                    r.Photos,
                    r.Raiting,
                    r.StartWorkTime,
                    r.EndWorkTime,
                    r.SchemeId).Value)
                .ToList();

            return restaurants;
        }

        public async Task<Guid> Create(Restaurant restaurant)
        {
            var restaurantEntity = new RestaurantEntity
            {
                Id = restaurant.Id,
                Title = restaurant.Title,
                Subtitle = restaurant.Subtitle,
                Description = restaurant.Description,
                UserId = restaurant.UserId,
                Address = restaurant.Address,
                Kitchen = restaurant.Kitchen,
                MenuIds = restaurant.MenuIds,
                Photos = restaurant.Photos,
                Raiting = restaurant.Raiting,
                StartWorkTime = restaurant.StartWorkTime,
                EndWorkTime = restaurant.EndWorkTime,
                SchemeId = restaurant.SchemeId
            };

            await _dbContext.AddAsync(restaurantEntity);
            await _dbContext.SaveChangesAsync();

            return restaurant.Id;
        }

        public async Task<Guid?> Update(Guid id, string title, string subtitle, string description, string address, string kitchen,
            List<Guid> menuIds, List<string> photos, string startWorkTime, string endWorkTime)
        {
            var restaurantEntity = await _dbContext.Restaurants.FirstOrDefaultAsync(r => r.Id == id);

            if (restaurantEntity == null) return null;

            restaurantEntity.Title = title;
            restaurantEntity.Subtitle = subtitle;
            restaurantEntity.Description = description;
            restaurantEntity.Address = address;
            restaurantEntity.Kitchen = kitchen;
            restaurantEntity.MenuIds = menuIds;
            restaurantEntity.Photos = photos;
            restaurantEntity.StartWorkTime = startWorkTime;
            restaurantEntity.EndWorkTime = endWorkTime;

            await _dbContext.SaveChangesAsync();

            return id;
        }
    }
}
