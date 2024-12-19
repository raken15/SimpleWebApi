using System;
using MyWebApi.Models;

namespace MyWebApi.Data;

public interface IItemsRepository
{
    void AddItem(ItemRequestModel item);
    void DeleteItem(int id);
    void UpdateItem(Item item);
    Item GetItem(int id);
    IEnumerable<Item> GetAllItems();
    int GetMaxId();
    bool ItemAlreadyExists(Item item);
}

