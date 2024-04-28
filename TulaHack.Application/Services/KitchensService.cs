using TulaHack.Core.Models;
using TulaHack.DataAccess.Repositories;

namespace TulaHack.Application.Services
{
    public class KitchensService
    {
        private readonly KitchensRepository _kitchensRepository;

        public KitchensService(KitchensRepository usersRepository)
        {
            _kitchensRepository = usersRepository;
        }

        public async Task<List<Kitchen>> GetKitchens()
        {
            return await _kitchensRepository.Get();
        }
    }
}
