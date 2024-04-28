using Microsoft.AspNetCore.Mvc;
using TulaHack.API.Contracts;
using TulaHack.Application.Services;
using TulaHack.Core.Models;

namespace TulaHack.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly MenusService _menusService;

        public MenuController(MenusService menusService)
        {
            _menusService = menusService;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<List<MenuResponse>>> GetMenusByResourantId(Guid id)
        {
            var menus = await _menusService.GetMenusByRestourantId(id);

            var response = menus
                .Select(m => new MenuResponse(
                    m.Id,
                    m.Name,
                    m.RestaurantId,
                    new RestaurantResponse(
                        m.Restaurant.Id,
                        m.Restaurant.Title,
                        m.Restaurant.Subtitle,
                        m.Restaurant.Description,
                        m.Restaurant.UserId,
                        null,
                        m.Restaurant.Address,
                        m.Restaurant.Kitchen,
                        m.Restaurant.MenuIds,
                        m.Restaurant.Photos,
                        m.Restaurant.Raiting,
                        m.Restaurant.StartWorkTime,
                        m.Restaurant.EndWorkTime,
                        m.Restaurant.SchemeId
                        ),
                    m.Price,
                    m.Description,
                    m.Photo
                    )
                ).ToList();

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateMenu([FromBody] MenuRequest request)
        {
            var menu = Menu.Create(
                Guid.NewGuid(),
                request.name,
                request.resourantId,
                null,
                request.price,
                request.description,
                request.photo
                );

            if (menu.IsFailure) return BadRequest(menu.Error);

            await _menusService.CreateMenu(menu.Value);

            return Ok(menu.Value.Id);
        }
    }
}
