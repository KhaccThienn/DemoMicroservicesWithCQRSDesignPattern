namespace MicroservicesWithCQRSDesignPattern.Interfaces
{
    public interface IRepository<K, T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(K id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task SaveAsync();
    }
}
