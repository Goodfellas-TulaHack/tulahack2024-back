using Microsoft.EntityFrameworkCore;
using TulaHack.Core.Models;

namespace TulaHack.DataAccess.Repositories
{
    public class KitchensRepository
    {
        private readonly TulaHackDbContext _dbContext;

        public KitchensRepository(TulaHackDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Kitchen>> Get()
        {
            var kitchenEntity = await _dbContext.Kitchens
                .AsNoTracking()
                .ToListAsync();

            var kitchens = kitchenEntity
                .Select(k => Kitchen.Create(k.Id, k.Name).Value).ToList();

            return kitchens;
        }
    }
}
