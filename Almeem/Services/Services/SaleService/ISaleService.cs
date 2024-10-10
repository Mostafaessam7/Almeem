using Services.Interfaces;
using Services.Services.ProductService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.SaleService
{
    public interface ISaleService
    {
        Task<IReadOnlyList<SaleDto>> GetAllAsync();
        Task<SaleDto?> GetByIdAsync(int id);
        Task<IReadOnlyList<SaleDto>> GetActiveSalesAsync();
        Task<SaleDto> CreateAsync(SaleDto saleDto);
        Task<bool> UpdateAsync(SaleDto saleDto);
        Task<bool> DeleteAsync(int id);

        // Task<IReadOnlyList<SaleDto>> GetSalesByCategoryAsync(int categoryId);
        // Task<IReadOnlyList<SaleDto>> GetSalesByProductAsync(int productId);
    }
}
