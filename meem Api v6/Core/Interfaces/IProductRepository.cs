using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<IReadOnlyList<Product>> GetProductsAsync(string? category, string? sort, string? search);
    Task<Product?> GetProductByIdAsync(int id);
    Task<IReadOnlyList<Category>> GetCategoriesAsync();

    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(Product product);
    bool ProductExists(int id);
    Task<bool> SaveChangesAsync();
}
