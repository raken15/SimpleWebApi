using System;

namespace MyWebApi.Models;

public class Item : ItemRequestModel
{
    public int Id { get; set; }
    public Item(int id, string name, decimal price, DateTime createdDate)
        : base(name, price, createdDate)
    {
        Id = id;
    }
}

