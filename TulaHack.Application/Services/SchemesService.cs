using TulaHack.Core.Models;
using TulaHack.DataAccess.Repositories;

namespace TulaHack.Application.Services
{
    public class SchemesService
    {
        private readonly SchemesRepository _schemesRepository;

        public SchemesService(SchemesRepository schemesRepository)
        {
            _schemesRepository = schemesRepository;
        }

        public async Task<Scheme> GetScheme(Guid id)
        {
            return await _schemesRepository.Get(id);
        }

        public async Task<Guid> CreateScheme(Scheme scheme)
        {
            return await _schemesRepository.Create(scheme);
        }

        public async Task<Guid?> UpdateScheme(Guid id, List<Guid> tableIds)
        {
            return await _schemesRepository.Update(id, tableIds);
        }
    }
}
