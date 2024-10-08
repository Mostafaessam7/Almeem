using Core.Context;
using Core.Entities;
using Infrastructure.Interfaces;

namespace Infrastructure.Repository
{
    public class ColorRepository(AlmeemContext context) : GenericRepository<ProductColor>(context), IColorRepository
    {
    }
}
