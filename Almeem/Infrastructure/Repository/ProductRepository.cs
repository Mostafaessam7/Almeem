using Core.Context;
using Core.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class ProductRepository(AlmeemContext context) : GenericRepository<Product>(context), IProductRepository
    {
        public async Task<IReadOnlyList<Product>> SearchByName(string input)
           => await context.Products.Include(p => p.Category).Include(p => p.ProductSizeColors).ThenInclude(x => x.ProductColor).Include(x => x.ProductSizeColors).ThenInclude(x => x.ProductSize).Where(x => x.NameInEnglish.Contains(input) || x.NameInArabic.Contains(input)).ToListAsync();

        public async Task<IReadOnlyList<Product>> FilterByCategory(int categoryId)
            => await context.Products.Include(p => p.Category).Include(p => p.ProductSizeColors).ThenInclude(x => x.ProductColor).Include(x => x.ProductSizeColors).ThenInclude(x => x.ProductSize).Where(p => p.CategoryId == categoryId).ToListAsync();

        public async Task<IReadOnlyList<Product>> GetNewArrival()
            => await context.Products.Include(p => p.Category).Include(p => p.ProductSizeColors).ThenInclude(x => x.ProductColor).Include(x => x.ProductSizeColors).ThenInclude(x => x.ProductSize).Where(p => p.IsNewArrival).ToListAsync();

        public async Task<IReadOnlyList<Product>> GetAsync()
            => await context.Products.Include(p => p.Category).Include(p => p.ProductSizeColors).ThenInclude(x => x.ProductColor).Include(x => x.ProductSizeColors).ThenInclude(x => x.ProductSize).ToListAsync();

        public async Task<Product?> GetByIdAsync(int id)
            => await context.Products.Include(p => p.Category).Include(p => p.ProductSizeColors).ThenInclude(x => x.ProductColor).Include(x => x.ProductSizeColors).ThenInclude(x => x.ProductSize).FirstOrDefaultAsync(x => x.Id == id);
    }
}
