using Microsoft.EntityFrameworkCore;
using TulaHack.Core.Models;
using TulaHack.DataAccess.Models;

namespace TulaHack.DataAccess.Repositories
{
    public class TablesRepository
    {
        private readonly TulaHackDbContext _dbContext;

        public TablesRepository(TulaHackDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Table>> GetBySchemeId(Guid id)
        {
            var tableEntities = await _dbContext.Tables
                .AsNoTracking()
                .Include(t => t.Scheme)
                .Where(t => t.SchemeId == id)
                .ToListAsync();

            var tables = tableEntities
                .Select(t => Table.Create(
                    t.Id,
                    t.SchemeId,
                    Scheme.Create(
                        t.Scheme.Id,
                        t.Scheme.RestaurantId,
                        null,
                        t.Scheme.TableIds
                    ).Value,
                    t.Persons,
                    t.X,
                    t.Y,
                    t.Width,
                    t.Height,
                    t.ScaleX,
                    t.ScaleY,
                    t.Rotate,
                    t.Radius,
                    t.Fill,
                    t.Type
                    ).Value
                ).ToList();

            return tables;
        }

        public async Task<Guid> Create(Table table)
        {
            var tableEntity = new TableEntity
            {
                Id = table.Id,
                SchemeId = table.SchemeId,
                Persons = table.Persons,
                X = table.X,
                Y = table.Y,
                Width = table.Width,
                Height = table.Height,
                ScaleX = table.ScaleX,
                ScaleY = table.ScaleY,
                Rotate = table.Rotate,
                Radius = table.Radius,
                Fill = table.Fill,
                Type = table.Type,
            };

            await _dbContext.AddAsync(tableEntity);
            await _dbContext.SaveChangesAsync();

            return table.Id;
        }

        public async Task<Guid?> Update(Guid id, float x, float y, float width, float height, int persons, float scaleX, float scaleY, float rotate,
            float radius, string fill, string type)
        {
            var tableEntity = await _dbContext.Tables.FirstOrDefaultAsync(t => t.Id == id);

            if (tableEntity == null) return null;

            tableEntity.X = x;
            tableEntity.Y = y;
            tableEntity.Width = width;
            tableEntity.Height = height;
            tableEntity.ScaleX = scaleX;
            tableEntity.ScaleY = scaleY;
            tableEntity.Rotate = rotate;
            tableEntity.Radius = radius;
            tableEntity.Fill = fill;
            tableEntity.Type = type;
            tableEntity.Persons = persons;

            await _dbContext.SaveChangesAsync();

            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _dbContext.Tables
                .Where(t => t.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
