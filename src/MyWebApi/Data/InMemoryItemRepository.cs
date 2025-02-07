using MyWebApi.Models;

namespace MyWebApi.Data;

public class InMemoryItemRepository : InMemoryRepository<Item>
{
    public InMemoryItemRepository() : base() { }
    public InMemoryItemRepository(IEnumerable<Item> entities) : base(entities) { }
    // Implement Update for Item
    public override async Task UpdateAsync(Item item)
    {
        var existingItem = await GetByIdAsync(item.Id);
        if (existingItem != null)
        {
            existingItem.Name = item.Name;
            existingItem.Price = item.Price;
            existingItem.CreatedDate = item.CreatedDate;
        }
    }
    public override async Task<bool> IsEntityAlreadyExistsAsync(Item entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        // Use Task.Run to simulate async for CPU-bound operations
        return await Task.Run(() =>
        {
            return _entities.Any(i => i.Id == entity.Id || i.Name == entity.Name);
        });
    }
}
