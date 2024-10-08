using AutoMapper;
using Core.Context;
using Core.Entities;
using Infrastructure.Interfaces;
using Services.Services.CategoryService.Dto;

namespace Services.Services.CategoryService
{
    public class CategoryService(ICategoryRepository repo, IMapper mapper, AlmeemContext context) : ICategoryService
    {
        public void Add(CategoryDto dto)
        {
            var mappedCategory = mapper.Map<Category>(dto);
            repo.Add(mappedCategory);
        }

        public void Delete(int id)
        {
            var category = context.Categories.Find(id);
            repo.Delete(category);
        }

        public bool entityExist(int id)
            => repo.entityExist(id);

        public async Task<IReadOnlyList<CategoryDto>> GetAsync()
        {
            var Categories = await repo.GetAsync();
            var mappedCategories = new List<CategoryDto>();

            foreach (var category in Categories)
            {
                mappedCategories.Add(mapper.Map<CategoryDto>(category));
            }

            return mappedCategories;
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await repo.GetByIdAsync(id);

            return mapper.Map<CategoryDto>(category);
        }

        public Task<bool> SaveChangesAsync()
            => repo.SaveChangesAsync();

        public void Update(int? id, CategoryDto dto)
        {
            var categoty = context.Categories.Find(id);
            var mappedCategory = mapper.Map(dto, categoty);

            repo.Update(mappedCategory);
        }
    }
}
