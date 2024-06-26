﻿using Microsoft.EntityFrameworkCore;
using TulaHack.Core.Models;
using TulaHack.DataAccess.Models;

namespace TulaHack.DataAccess.Repositories
{
    public class SchemesRepository
    {
        private readonly TulaHackDbContext _dbContext;

        public SchemesRepository(TulaHackDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Scheme> Get(Guid id)
        {
            var schemeEntity = await _dbContext.Schemes
                .AsNoTracking()
                .Include(s => s.Restaurant)
                .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(s => s.Id == id);

            return Scheme.Create(
                schemeEntity.Id,
                schemeEntity.RestaurantId,
                Restaurant.Create(
                    schemeEntity.Restaurant.Id,
                    schemeEntity.Restaurant.Title,
                    schemeEntity.Restaurant.Subtitle,
                    schemeEntity.Restaurant.Description,
                    schemeEntity.Restaurant.UserId,
                    User.Create(
                        schemeEntity.Restaurant.User.Id,
                        schemeEntity.Restaurant.User.Login,
                        string.Empty,
                        schemeEntity.Restaurant.User.Role,
                        schemeEntity.Restaurant.User.FirstName,
                        schemeEntity.Restaurant.User.LastName,
                        schemeEntity.Restaurant.User.MiddleName,
                        schemeEntity.Restaurant.User.Phone
                        ).Value,
                    schemeEntity.Restaurant.Address,
                    schemeEntity.Restaurant.Kitchens,
                    schemeEntity.Restaurant.MenuIds,
                    schemeEntity.Restaurant.Photos,
                    schemeEntity.Restaurant.Raiting,
                    schemeEntity.Restaurant.StartWorkTime,
                    schemeEntity.Restaurant.EndWorkTime,
                    schemeEntity.Restaurant.SchemeId
                    ).Value,
                schemeEntity.TableIds
                ).Value;
        }

        public async Task<Guid> Create(Scheme scheme)
        {
            var schemeEntity = new SchemeEntity
            {
                Id = scheme.Id,
                RestaurantId = scheme.RestaurantId,
                TableIds = scheme.TableIds
            };

            await _dbContext.AddAsync(schemeEntity);
            await _dbContext.SaveChangesAsync();

            return scheme.Id;
        }

        public async Task<Guid?> Update(Guid id, List<Guid> tableIds)
        {
            var schemeEntity = await _dbContext.Schemes.FirstOrDefaultAsync(s => s.Id == id);

            if (schemeEntity == null) return null;

            schemeEntity.TableIds = tableIds;

            await _dbContext.SaveChangesAsync();

            return id;
        }
    }
}
