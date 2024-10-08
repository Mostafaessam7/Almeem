using Core.Context;
using Core.Entities;
using Infrastructure.Interfaces;

namespace Infrastructure.Repository
{
    public class CategoryRepository(AlmeemContext context) : GenericRepository<Category> (context), ICategoryRepository
    {
    }
}
