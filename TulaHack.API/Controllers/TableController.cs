using Microsoft.AspNetCore.Mvc;
using TulaHack.API.Contracts;
using TulaHack.Application.Services;
using TulaHack.Core.Models;

namespace TulaHack.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TableController : ControllerBase
    {
        private readonly TablesService _tablesService;

        public TableController(TablesService tablesService)
        {
            _tablesService = tablesService;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<List<TableResponse>>> GetTablesBySchemeId(Guid id)
        {
            var tables = await _tablesService.GetTablesBySchemeId(id);

            var response = tables
                .Select(t => new TableResponse(
                    t.Id,
                    t.SchemeId,
                    new SchemeResponse(
                        t.Scheme.Id,
                        t.Scheme.RestaurantId,
                        null,
                        t.Scheme.TableIds
                        ),
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
                    )
                ).ToList();

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateTable([FromBody] TableRequest request)
        {
            var table = Table.Create(
                request.id,
                request.schemeId,
                null,
                request.numberOfPeople,
                request.x,
                request.y,
                request.width,
                request.height,
                request.scaleX,
                request.scaleY,
                request.rotation,
                request.radius,
                request.fill,
                request.type
                );

            if (table.IsFailure) return BadRequest(table.Error);

            return await _tablesService.CreateTable(table.Value);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid?>> UpdateTable(Guid id, [FromBody] TableRequest request)
        {
            var table = Table.Create(
                request.id,
                request.schemeId,
                null,
                request.numberOfPeople,
                request.x,
                request.y,
                request.width,
                request.height,
                request.scaleX,
                request.scaleY,
                request.rotation,
                request.radius,
                request.fill,
                request.type
                );

            if (table.IsFailure) return BadRequest(table.Error);

            return await _tablesService.UpdateTable(
                id,
                table.Value.X,
                table.Value.Y,
                request.width,
                request.height,
                request.numberOfPeople,
                request.scaleX,
                request.scaleY,
                request.rotation,
                request.radius,
                request.fill,
                request.type
                );
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid?>> DeleteTable(Guid id)
        {
            return await _tablesService.DeleteTable(id);
        }
    }
}
