using API.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseApiController
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CategoryDto>>> GetCategories()
        {
            var categories = await _categoryRepository.GetCategoriesAsync();
            var categoryDtos = categories.Select(MapCategoryToDto).ToList();
            return Ok(categoryDtos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(MapCategoryToDto(category));
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
        {
            var category = new Category
            {
                NameEn = createCategoryDto.NameEn,
                NameAr = createCategoryDto.NameAr
            };

            _categoryRepository.Add(category);
            var result = await _categoryRepository.SaveAllAsync();

            if (result)
            {
                var categoryDto = MapCategoryToDto(category);
                return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, categoryDto);
            }

            return BadRequest("Failed to create category");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryDto>> UpdateCategory(int id, [FromBody] CreateCategoryDto updateCategoryDto)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            category.NameEn = updateCategoryDto.NameEn;
            category.NameAr = updateCategoryDto.NameAr;

            _categoryRepository.Update(category);
            var result = await _categoryRepository.SaveAllAsync();

            if (result)
            {
                return Ok(MapCategoryToDto(category));
            }

            return BadRequest("Failed to update category");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            _categoryRepository.Remove(category);
            var result = await _categoryRepository.SaveAllAsync();

            if (result)
            {
                return NoContent();
            }

            return BadRequest("Failed to delete category");
        }
        [HttpGet("count")]
        public async Task<ActionResult<int>> GetCategoryCount()
        {
            var count = await _categoryRepository.GetCategoryCountAsync();
            return Ok(count);
        }


        // Mapping methods
        private CategoryDto MapCategoryToDto(Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                NameEn = category.NameEn,
                NameAr = category.NameAr
            };
        }
    }
}
