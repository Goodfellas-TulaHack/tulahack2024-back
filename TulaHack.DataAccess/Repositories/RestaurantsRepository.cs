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
                .OrderBy(r => r.Title)
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
                    r.Kitchens,
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
                    restaurantEntity.Kitchens,
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
                    r.Kitchens,
                    r.MenuIds,
                    r.Photos,
                    r.Raiting,
                    r.StartWorkTime,
                    r.EndWorkTime,
                    r.SchemeId).Value)
                .ToList();

            return restaurants;
        }

        public async Task<List<Restaurant>> GetByFilter(string title, List<Guid> kitchenIds)
        {
            var query = _dbContext.Restaurants.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
            {
                query = query.Where(r => r.Title.ToLower().Contains(title));
            }

            if (kitchenIds.Count > 0)
            {
                foreach (var kitcheId in kitchenIds)
                {
                    query = query.Where(r => r.Kitchens.Contains(kitcheId));
                }
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
                    r.Kitchens,
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
                Kitchens = restaurant.Kitchen,
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

        public async Task<Guid?> Update(Guid id, string title, string subtitle, string description, string address, List<Guid> kitchen,
            List<Guid> menuIds, List<string> photos, string startWorkTime, string endWorkTime)
        {
            var restaurantEntity = await _dbContext.Restaurants.FirstOrDefaultAsync(r => r.Id == id);

            if (restaurantEntity == null) return null;

            if (!string.IsNullOrEmpty(title)) restaurantEntity.Title = title;
            if (!string.IsNullOrEmpty(subtitle)) restaurantEntity.Subtitle = subtitle;
            if (!string.IsNullOrEmpty(description)) restaurantEntity.Description = description;
            if (!string.IsNullOrEmpty(address)) restaurantEntity.Address = address;
            if (menuIds.Count != 0) restaurantEntity.MenuIds = menuIds;
            if (photos.Count != 0) restaurantEntity.Photos = photos;
            if (!string.IsNullOrEmpty(startWorkTime)) restaurantEntity.StartWorkTime = startWorkTime;
            if (!string.IsNullOrEmpty(endWorkTime)) restaurantEntity.EndWorkTime = endWorkTime;

            await _dbContext.SaveChangesAsync();

            return id;
        }
    }
}
