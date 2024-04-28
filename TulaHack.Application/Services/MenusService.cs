using TulaHack.Core.Models;
using TulaHack.DataAccess.Repositories;

namespace TulaHack.Application.Services
{
    public class MenusService
    {
        private readonly MenusRepository _menusRepository;

        public MenusService(MenusRepository menusRepository)
        {
            _menusRepository = menusRepository;
        }

        public async Task<List<Menu>> GetMenusByRestourantId(Guid id)
        {
            return await _menusRepository.GetByRestourantId(id);
        }

        public async Task<Guid> CreateMenu(Menu menu)
        {
            return await _menusRepository.Create(menu);
        }
    }
}
