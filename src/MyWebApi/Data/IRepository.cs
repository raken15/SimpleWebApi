namespace MyWebApi.Data;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteByIdAsync(int id);
    Task<int> GetMaxIdAsync();
    Task<bool> IsEntityAlreadyExistsAsync(T entity);
}
