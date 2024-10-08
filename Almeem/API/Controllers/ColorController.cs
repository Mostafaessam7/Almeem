using Api.Controllers;
using Core.Entities;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Services.Services.ColorService;
using Services.Services.ColorService.Dto;

namespace API.Controllers
{
    public class ColorController(IColorService service) : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ColorDto>>> GetProductColors()
        {
            return Ok(await service.GetAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ColorDto>> GetColorById(int id)
        {
            var color = await service.GetByIdAsync(id);

            if (color == null) return NotFound();

            return color;
        }

        [HttpPost]
        public async Task<ActionResult<ColorDto>> CreateColor(ColorDto color)
        {
            service.Add(color);
            if (await service.SaveChangesAsync())
            {
                return Ok(color);
            }
            return BadRequest("Problem Creating Product Color");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateColor(int id, ColorDto color)
        {
            if (!ColorExists(id))
                return BadRequest("Can not update this color");

            service.Update(id, color);

            if (await service.SaveChangesAsync())
                return NoContent();

            return BadRequest("Problem Updating color");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteColor(int id)
        {
            var color = await service.GetByIdAsync(id);

            if (color == null) return NotFound();

            service.Delete(id);

            if (await service.SaveChangesAsync())
                return NoContent();

            return BadRequest("Problem Deleting Color");
        }

        private bool ColorExists(int id)
        {
            return service.entityExist(id);
        }
    }
}
