using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly StoreContext _context;

        public CategoryRepository(StoreContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await GetByIdAsync(id);
        }
        public async Task<int> GetCategoryCountAsync()
        {
            return await _context.Categories.CountAsync();
        }
    }

}
