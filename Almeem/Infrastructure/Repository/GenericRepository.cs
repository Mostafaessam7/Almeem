using Core.Context;
using Core.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GenericRepository<T>(AlmeemContext context) : IGenericRepository<T> where T : BaseEntity
    {
        public void Add(T entity)
            => context.Set<T>().Add(entity);

        public void Delete(T entity)
            => context.Set<T>().Remove(entity);

        public bool entityExist(int id)
            => context.Set<T>().Any(x => x.Id == id);

        public async Task<IReadOnlyList<T>> GetAsync()
            => await context.Set<T>().ToListAsync();

        public async Task<T?> GetByIdAsync(int id)
            => await context.Set<T>().FindAsync(id);

        public async Task<bool> SaveChangesAsync()
            => await context.SaveChangesAsync() > 0;

        public void Update(T entity)
        {
            context.Set<T>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            //context.Set<T>().Update(entity);
        }
    }
}
