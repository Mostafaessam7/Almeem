using Services.Interfaces;
using Services.Services.ProductService.Dto;

namespace Services.Services.ProductService
{
    public interface IProductService : IGenericService<ProductDto>
    {
        Task<IReadOnlyList<ProductDto>> SearchByName(string input);
        Task<IReadOnlyList<ProductDto>> GetNewArrival();
        Task<IReadOnlyList<ProductDto>> FilterByCategory(int categoryId);
    }
}
