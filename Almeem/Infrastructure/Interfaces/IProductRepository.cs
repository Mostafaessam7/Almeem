using Core.Entities;

namespace Infrastructure.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IReadOnlyList<Product>> SearchByName(string input);
        Task<IReadOnlyList<Product>> GetNewArrival();
        Task<IReadOnlyList<Product>> GetAsync();
        Task<IReadOnlyList<Product>> FilterByCategory(int categoryId);
        Task<Product> GetByIdAsync(int id);
    }
}
