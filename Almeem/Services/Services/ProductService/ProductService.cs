using AutoMapper;
using Core.Context;
using Core.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Services.Services.ProductService.Dto;
using Services.Services.ProductSizeColorService.Dto;

namespace Services.Services.ProductService
{
    public class ProductService(IProductRepository repo, IMapper mapper, AlmeemContext context) : IProductService
    {
        public async void Add(ProductDto dto)
            => repo.Add(await MapDtoToEntity(null,dto));

        public async void Delete(int id)
        {
            var product = context.Products.Find(id);

            if (product == null)
                return;

            repo.Delete(product);
        }

        public bool entityExist(int id)
            => repo.entityExist(id);

        public async Task<IReadOnlyList<ProductDto>> GetAsync()
            => await MapEntityListToDtoList(await repo.GetAsync());

        public async Task<ProductDto?> GetByIdAsync(int id)
            => await MapEntityToDto(await repo.GetByIdAsync(id));

        public Task<bool> SaveChangesAsync()
            => repo.SaveChangesAsync();

        public async void Update(int? id, ProductDto dto)
            => repo.Update(await MapDtoToEntity(id, dto));

        public async Task<IReadOnlyList<ProductDto>> GetNewArrival()
            => await MapEntityListToDtoList(await repo.GetNewArrival());

        public async Task<IReadOnlyList<ProductDto>> FilterByCategory(int categoryId)
            => await MapEntityListToDtoList(await repo.FilterByCategory(categoryId));

        public async Task<IReadOnlyList<ProductDto>> SearchByName(string input)
            => await MapEntityListToDtoList(await repo.SearchByName(input));

        private async Task<Product> MapDtoToEntity(int? id, ProductDto dto)
        {
            try
            {
                var product = context.Products.Find(id);
                
                var mappedProduct = mapper.Map(dto, product);
                var category = context.Categories.FirstOrDefault(c => c.Name.ToLower() == dto.CategoryName.ToLower());

                if (category == null)
                    throw new Exception("Category Not Found");

                mappedProduct.Category = category;
                mappedProduct.CategoryId = category.Id;

                if (mappedProduct.ProductSizeColors == null)
                    mappedProduct.ProductSizeColors = new List<ProductSizeColor>();

                foreach(var productSizeColorDto in dto.ProductSizeColorDto)
                {
                    var productSizeColor = new ProductSizeColor();

                    var size = context.ProductSizes.FirstOrDefault(s => s.Size.ToLower() == productSizeColorDto.Size.ToLower());

                    if(size == null)
                        throw new Exception("Size Not Found");

                    productSizeColor.ProductSize = size;
                    productSizeColor.ProductSizeId = size.Id;

                    var color = context.ProductColors.FirstOrDefault(c => c.Name.ToLower() == productSizeColorDto.Color.ToLower());

                    if (color == null)
                        throw new Exception("Color Not Found");

                    productSizeColor.ProductColor = color;
                    productSizeColor.ProductColorId = color.Id;

                    mappedProduct.TotalQuantity += productSizeColorDto.StockQuantity;

                    mappedProduct.ProductSizeColors.Add(productSizeColor);
                }

                return mappedProduct;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<ProductDto> MapEntityToDto(Product product)
        {
            try
            {
                var dto = mapper.Map<ProductDto>(product);

                if (dto == null)
                    throw new Exception("Can not Map Product");

                if(dto.ProductSizeColorDto == null)
                    dto.ProductSizeColorDto = new List<ProductSizeColorDto>();

                foreach (var item in product.ProductSizeColors)
                {
                    var productSizeColorDto = mapper.Map<ProductSizeColorDto>(item);
                    dto.ProductSizeColorDto.Add(productSizeColorDto);
                }

                return dto;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<IReadOnlyList<ProductDto>> MapEntityListToDtoList(IReadOnlyList<Product> products)
        {
            var dtos = new List<ProductDto>();
            foreach (var product in products)
            {
                var dto = await MapEntityToDto(product);
                dtos.Add(dto);
            }

            return dtos;
        }
    }
}
