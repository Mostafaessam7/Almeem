using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface ISaleRepository : IGenericRepository<Sale>
    {
        Task<IReadOnlyList<Sale>> GetActiveSalesAsync();
        Task<IReadOnlyList<Sale>> GetSalesByCategoryAsync(int categoryId);
        Task<IReadOnlyList<Sale>> GetSalesByProductAsync(int productId);

    }
}
