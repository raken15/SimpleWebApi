using MyWebApi.Models;

namespace MyWebApi.Data;

public interface IItemRepository
{
    Item AddItem(Item item);
    void DeleteItem(int id);
    void UpdateItem(Item item);
    Item GetItem(int id);
    IEnumerable<Item> GetAllItems();
    int GetMaxId();
    bool IsItemAlreadyExists(Item item);
}

