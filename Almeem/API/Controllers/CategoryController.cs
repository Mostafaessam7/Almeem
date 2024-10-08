using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Services.CategoryService;
using Services.Services.CategoryService.Dto;

namespace Api.Controllers
{
    public class CategoryController(ICategoryService service) : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CategoryDto>>> GetCategorys()
        {
            return Ok(await service.GetAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryById(int id)
        {
            var category = await service.GetByIdAsync(id);

            if (category == null) return NotFound();

            return category;
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory(CategoryDto category)
        {
            service.Add(category);
            if (await service.SaveChangesAsync())
            {
                return Ok(category);
            }
            return BadRequest("Problem Creating Category");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateCategory(int id, CategoryDto category)
        {
            if (!CategoryExists(id))
                return BadRequest("Can not update this category");

            service.Update(id, category);

            if (await service.SaveChangesAsync())
                return NoContent();

            return BadRequest("Problem Updating category");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var category = await service.GetByIdAsync(id);

            if (category == null) return NotFound();

            service.Delete(id);

            if (await service.SaveChangesAsync())
                return NoContent();

            return BadRequest("Prblem Deleting Category");
        }

        private bool CategoryExists(int id)
        {
            return service.entityExist(id);
        }
    }
}
