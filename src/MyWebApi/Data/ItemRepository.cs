using MyWebApi.Models;

namespace MyWebApi.Data;

public class ItemsRepository : IItemRepository
{
    private List<Item> _items;
    public ItemsRepository()
    {
        _items = new List<Item>()
        {
            new Item(1, "Item 1", 10.99m, DateTime.Now),
            new Item(2, "Item 2", 9.99m, DateTime.Now),
            new Item(3, "Item 3", 5.99m, DateTime.Now)
        };
    }

    public ItemsRepository(List<Item> items)
    {
        _items = items;
        if(_items == null)
        {
            _items = new List<Item>();
        }
    }

    public void AddItem(Item item)
    {
        if(item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }
        else
        {
            var maxId = GetMaxId();
            var newItem = new Item(maxId + 1
            , item.Name, item.Price, item.CreatedDate);
            if(ItemAlreadyExists(newItem))
            {
                throw new ArgumentException("Item already exists");
            }
            _items.Add(newItem);
        }
    }

    public Item GetItem(int id)
    {
        var item = _items.Find(i => i.Id == id);
        if (item == null)
        {
            throw new KeyNotFoundException($"Item with id {id} does not exist");
        }
        return item;
    }

    public void UpdateItem(Item item)
    {
        var existingItem = GetItem(item.Id);
        if (existingItem != null)
        {
            existingItem.Name = item.Name;
            existingItem.Price = item.Price;
            existingItem.CreatedDate = item.CreatedDate;
        }
    }

    public IEnumerable<Item> GetAllItems()
    {
        return _items;
    }
    public void DeleteItem(int id)
    {
        var itemToRemove = _items.Find(i => i.Id == id);
        if (itemToRemove != null)
        {
            _items.Remove(itemToRemove);
        }
    }
    public int GetMaxId()
    {
        var maxId = _items.Any() ? _items.Max(i => i.Id) : 0;
        return maxId;
    }
    public bool ItemAlreadyExists(Item item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }
        else
        {
            return _items.Any(i =>  i.Id == item.Id ||
            i.Name == item.Name);
        }
    }

}
