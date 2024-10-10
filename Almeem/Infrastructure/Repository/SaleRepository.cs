using Core.Context;
using Core.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class SaleRepository : GenericRepository<Sale>, ISaleRepository
    {

        private readonly AlmeemContext _context;

        public SaleRepository(AlmeemContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Sale>> GetActiveSalesAsync()
        {
            var currentDate = DateTime.Now;
            return await _context.Sales
                .Include(s => s.Products)
                .Include(s => s.Categories)
                .Where(s => s.StartDate <= currentDate && s.EndDate >= currentDate)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Sale>> GetSalesByCategoryAsync(int categoryId)
        {
            return await _context.Sales
                .Include(s => s.Categories)
                .Where(s => s.Categories.Any(c => c.Id == categoryId))
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Sale>> GetSalesByProductAsync(int productId)
        {
            return await _context.Sales
                .Include(s => s.Products)
                .Where(s => s.Products.Any(p => p.Id == productId))
                .ToListAsync();
        }
    }
}

