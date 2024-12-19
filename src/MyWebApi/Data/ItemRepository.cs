using System;
using MyWebApi.Models;

namespace MyWebApi.Data;

public class ItemRepository : IItemRepository
{
    private List<Item> _items;
    public ItemRepository()
    {
        _items = new List<Item>()
        {
            new Item()
            {
                Id = 1,
                Name = "Item 1",
                Price = 10.99m,
                CreatedDate = DateTime.Now
            },
            new Item()
            {
                Id = 2,
                Name = "Item 2",
                Price = 9.99m,
                CreatedDate = DateTime.Now
            },
            new Item()
            {
                Id = 3,
                Name = "Item 3",
                Price = 5.99m,
                CreatedDate = DateTime.Now
            }
        };
    }
    public ItemRepository(List<Item> items)
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
            if (!_items.Any(i => i.Id == item.Id))
            {
                _items.Add(item);
            }
            else
            {
                throw new ArgumentException("Key already exists", nameof(item.Id));
            }
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
}
