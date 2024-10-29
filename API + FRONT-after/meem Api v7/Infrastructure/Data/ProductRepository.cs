using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    private readonly StoreContext _context;

    public ProductRepository(StoreContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync(string? category, string? sort, string? search)
    {
        var spec = new ProductsWithCategorySpecification(new ProductSpecParams
        {
            Search = search,
            Sort = sort,
            CategoryId = !string.IsNullOrEmpty(category) ? (int?)int.Parse(category) : null
        });
        return await ListAsync(spec);
    }
    //public async Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecParams productParams)
    //{
    //    var spec = new ProductsWithCategorySpecification(productParams);
    //    return await ListAsync(spec);
    //}

    //Count useful for pagination 
    public async Task<int> GetProductsCountAsync(ProductSpecParams productParams)
    {
        var countSpec = new ProductsWithCategorySpecification(productParams);
        return await CountAsync(countSpec);
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        var spec = new ProductWithDetailsSpecification(id);
        return await GetEntityWithSpec(spec);
    }

    public async Task<IReadOnlyList<Category>> GetCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }


    public void AddProduct(Product product)
    {
        Add(product);
    }

    public void UpdateProduct(Product product)
    {
        Update(product);
    }

    public void DeleteProduct(Product product)
    {
        Remove(product);
    }

    public bool ProductExists(int id)
    {
        return Exists(id);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await SaveAllAsync();
    }
}
