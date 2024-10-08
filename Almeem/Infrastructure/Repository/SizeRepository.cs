using Core.Context;
using Core.Entities;
using Infrastructure.Interfaces;

namespace Infrastructure.Repository
{
    public class SizeRepository(AlmeemContext context) : GenericRepository<ProductSize>(context), ISizeRepository
    {
    }
}
