namespace Services.Interfaces
{
    public interface IGenericService<T>
    {
        Task<IReadOnlyList<T>> GetAsync();
        Task<T?> GetByIdAsync(int id);
        void Add(T entity);
        void Update(int? id, T entity);
        void Delete(int id);
        bool entityExist(int id);
        Task<bool> SaveChangesAsync();
    }
}
