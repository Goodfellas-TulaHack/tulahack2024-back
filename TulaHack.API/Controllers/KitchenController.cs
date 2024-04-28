using Microsoft.AspNetCore.Mvc;
using TulaHack.API.Contracts;
using TulaHack.Application.Services;

namespace TulaHack.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KitchenController : ControllerBase
    {
        private readonly KitchensService _kitchensService;

        public KitchenController(KitchensService kitchensService)
        {
            _kitchensService = kitchensService;
        }

        [HttpGet]
        public async Task<ActionResult<List<KitchenResponse>>> GetKitchens()
        {
            var kitchens = await _kitchensService.GetKitchens();

            var response = kitchens
                .Select(k => new KitchenResponse(k.Id, k.Name)).ToList();

            return Ok(response);
        }
    }
}
