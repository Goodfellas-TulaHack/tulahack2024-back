using Microsoft.EntityFrameworkCore;
using TulaHack.Core.Models;
using TulaHack.DataAccess.Models;

namespace TulaHack.DataAccess.Repositories
{
    public class MenusRepository
    {
        private readonly TulaHackDbContext _dbContext;

        public MenusRepository(TulaHackDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Menu>> Get()
        {
            var menuEntities = await _dbContext.Menus
                .AsNoTracking()
                .Include(m => m.Restaurant)
                .ToListAsync();

            var menus = menuEntities
                .Select(m => Menu.Create(
                    m.Id,
                    m.Name,
                    m.RestaurantId,
                    Restaurant.Create(
                        m.Restaurant.Id,
                        m.Restaurant.Title,
                        m.Restaurant.Subtitle,
                        m.Restaurant.Description,
                        m.Restaurant.UserId,
                        null,
                        m.Restaurant.Address,
                        m.Restaurant.Kitchens,
                        m.Restaurant.MenuIds,
                        m.Restaurant.Photos,
                        m.Restaurant.Raiting,
                        m.Restaurant.StartWorkTime,
                        m.Restaurant.EndWorkTime,
                        m.Restaurant.SchemeId
                        ).Value,
                    m.Price,
                    m.Description,
                    m.Photo
                    ).Value
                ).ToList();
            
            return menus;
        }

        public async Task<List<Menu>> GetByRestourantId(Guid id)
        {
            var menuEntities = await _dbContext.Menus
                .AsNoTracking()
                .Include(m => m.Restaurant)
                .Where(m => m.RestaurantId == id)
                .ToListAsync();

            var menus = menuEntities
                .Select(m => Menu.Create(
                    m.Id,
                    m.Name,
                    m.RestaurantId,
                    Restaurant.Create(
                        m.Restaurant.Id,
                        m.Restaurant.Title,
                        m.Restaurant.Subtitle,
                        m.Restaurant.Description,
                        m.Restaurant.UserId,
                        null,
                        m.Restaurant.Address,
                        m.Restaurant.Kitchens,
                        m.Restaurant.MenuIds,
                        m.Restaurant.Photos,
                        m.Restaurant.Raiting,
                        m.Restaurant.StartWorkTime,
                        m.Restaurant.EndWorkTime,
                        m.Restaurant.SchemeId
                        ).Value,
                    m.Price,
                    m.Description,
                    m.Photo
                    ).Value
                ).ToList();

            return menus;
        }

        public async Task<Guid> Create(Menu menu)
        {
            var menuEntity = new MenuEntity
            {
                Id = menu.Id,
                Name = menu.Name,
                RestaurantId = menu.RestaurantId,
                Price = menu.Price,
                Description = menu.Description,
                Photo = menu.Photo
            };

            await _dbContext.AddAsync(menuEntity);
            await _dbContext.SaveChangesAsync();

            return menu.Id;
        }
    }
}
