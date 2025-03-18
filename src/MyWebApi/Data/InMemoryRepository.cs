using MyWebApi.Models;

namespace MyWebApi.Data;

public abstract class InMemoryRepository<T> : IRepository<T> where T : class, IIdentifiable
{
    protected readonly List<T> _entities;

    public InMemoryRepository()
    {
        _entities = new List<T>();
    }
    /// <summary>
    /// Initializes a new instance of the InMemoryRepository with a predefined list of entities.
    /// </summary>
    /// <param name="items"></param>
    public InMemoryRepository(IEnumerable<T> entities)
    {
        _entities = entities?.ToList() ?? new List<T>();
    }
    public Task<IEnumerable<T>> GetAllAsync() =>
        Task.FromResult(_entities.AsEnumerable());

    public async Task<T?> GetByIdAsync(int id)
    {
        var entityToGet = _entities.FirstOrDefault(entity => entity.Id == id);
        if (entityToGet == null)
        {
            throw new KeyNotFoundException($"Item with id {id} does not exist");
        }
        return await Task.FromResult(entityToGet);
    }
    public Task AddAsync(T entity)
    {
        entity.Id = _entities.Any() ? _entities.Max(entity => entity.Id) + 1 : 1;
        _entities.Add(entity);
        return Task.CompletedTask;
    }
    // Abstract Update method that child classes must implement
    public abstract Task UpdateAsync(T item);
    public async Task DeleteByIdAsync(int id)
    {
        var entityToRemove = await GetByIdAsync(id);
        if (entityToRemove != null)
        {
            _entities.Remove(entityToRemove);
        }
    }
    public async Task<int> GetMaxIdAsync()
    {
        // Use Task.Run to simulate async for CPU-bound operations
        return await Task.Run(() =>
        {
            return _entities.Any() ? _entities.Max(i => i.Id) : 0;
        });
    }
    // Abstract IsEntityAlreadyExistsAsync method that child classes must implement
    public abstract Task<bool> IsEntityAlreadyExistsAsync(T entity);
}
