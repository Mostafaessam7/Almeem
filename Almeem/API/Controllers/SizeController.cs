using Api.Controllers;
using Core.Entities;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Services.Services.SizeService;
using Services.Services.SizeService.Dto;

namespace API.Controllers
{
    public class SizeController(ISizeService service) : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<SizeDto>>> GetProductSizes()
        {
            return Ok(await service.GetAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SizeDto>> GetSizeById(int id)
        {
            var size = await service.GetByIdAsync(id);

            if (size == null) return NotFound();

            return size;
        }

        [HttpPost]
        public async Task<ActionResult<SizeDto>> CreateSize(SizeDto size)
        {
            service.Add(size);
            if (await service.SaveChangesAsync())
            {
                return Ok(size);
            }
            return BadRequest("Problem Creating Product Size");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateSize(int id, SizeDto size)
        {
            if (!SizeExists(id))
                return BadRequest("Can not update this size");

            service.Update(id, size);

            if (await service.SaveChangesAsync())
                return NoContent();

            return BadRequest("Problem Updating size");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteSize(int id)
        {
            var size = await service.GetByIdAsync(id);

            if (size == null) return NotFound();

            service.Delete(id);

            if (await service.SaveChangesAsync())
                return NoContent();

            return BadRequest("Problem Deleting Size");
        }

        private bool SizeExists(int id)
        {
            return service.entityExist(id);
        }
    }
}
