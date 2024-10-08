using AutoMapper;
using Core.Context;
using Core.Entities;
using Infrastructure.Interfaces;
using Services.Services.CategoryService.Dto;
using Services.Services.ColorService.Dto;

namespace Services.Services.ColorService
{
    public class ColorService(IColorRepository repo, IMapper mapper, AlmeemContext context) : IColorService
    {
        public void Add(ColorDto dto)
        {
            var mappedColor = mapper.Map<ProductColor>(dto);
            repo.Add(mappedColor);
        }

        public void Delete(int id)
        {
            var color = context.ProductColors.Find(id);
            repo.Delete(color);
        }

        public bool entityExist(int id)
            => repo.entityExist(id);

        public async Task<IReadOnlyList<ColorDto>> GetAsync()
        {
            var colors = await repo.GetAsync();
            var mappedcolors = new List<ColorDto>();

            foreach (var color in colors)
            {
                mappedcolors.Add(mapper.Map<ColorDto>(color));
            }

            return mappedcolors;
        }

        public async Task<ColorDto?> GetByIdAsync(int id)
        {
            var category = await repo.GetByIdAsync(id);

            return mapper.Map<ColorDto>(category);
        }

        public Task<bool> SaveChangesAsync()
        => repo.SaveChangesAsync();

        public void Update(int? id, ColorDto dto)
        {
            var color = context.ProductColors.Find(id);
            var mappedColor = mapper.Map(dto, color);

            repo.Update(mappedColor);
        }
    }
}
