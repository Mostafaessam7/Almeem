using AutoMapper;
using Core.Context;
using Core.Entities;
using Infrastructure.Interfaces;
using Services.Services.ColorService.Dto;
using Services.Services.SizeService.Dto;

namespace Services.Services.SizeService
{
    public class SizeService(ISizeRepository repo, IMapper mapper, AlmeemContext context) : ISizeService
    {
        public void Add(SizeDto dto)
        {
            var mappedSize = mapper.Map<ProductSize>(dto);
            repo.Add(mappedSize);
        }

        public void Delete(int id)
        {
            var size = context.ProductSizes.Find(id);
            repo.Delete(size);
        }

        public bool entityExist(int id)
            => repo.entityExist(id);

        public async Task<IReadOnlyList<SizeDto>> GetAsync()
        {
            var sizes = await repo.GetAsync();
            var mappedSizes = new List<SizeDto>();

            foreach (var size in sizes)
            {
                mappedSizes.Add(mapper.Map<SizeDto>(size));
            }

            return mappedSizes;
        }

        public async Task<SizeDto?> GetByIdAsync(int id)
        {
            var category = await repo.GetByIdAsync(id);

            return mapper.Map<SizeDto>(category);
        }

        public Task<bool> SaveChangesAsync()
            => repo.SaveChangesAsync();

        public void Update(int? id, SizeDto dto)
        {
            var size = context.ProductSizes.Find(id);

            var mappedSize = mapper.Map(dto, size);

            repo.Update(mappedSize);
        }
    }
}
