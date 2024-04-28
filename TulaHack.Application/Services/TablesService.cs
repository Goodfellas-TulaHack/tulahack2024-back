using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using TulaHack.Core.Models;
using TulaHack.DataAccess.Repositories;

namespace TulaHack.Application.Services
{
    public class TablesService
    {
        private readonly TablesRepository _tablesRepository;

        public TablesService(TablesRepository tablesRepository)
        {
            _tablesRepository = tablesRepository;
        }

        public async Task<List<Table>> GetTablesBySchemeId(Guid id)
        {
            return await _tablesRepository.GetBySchemeId(id);
        }

        public async Task<Guid> CreateTable(Table table)
        {
            return await _tablesRepository.Create(table);
        }

        public async Task<Guid?> UpdateTable(Guid id, float x, float y, float width, float height, int persons,
            float scaleX, float scaleY, float rotate, float radius, string fill, string type)
        {
            return await _tablesRepository.Update(id, x, y, width, height, persons, scaleX, scaleY, rotate, radius, fill, type);
        }

        public async Task<Guid> DeleteTable(Guid id)
        {
            return await _tablesRepository.Delete(id);
        }
    }
}
