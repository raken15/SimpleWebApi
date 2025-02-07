using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MyWebApi.Models;

public class Item : IIdentifiable
{
    [BindNever]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [Range(1, 60)]
    public decimal Price { get; set; }
    public DateTime CreatedDate { get; set; }

    public Item(int id, string name, decimal price, DateTime createdDate)
    {
        Id = id;
        Name = name;
        Price = price;
        CreatedDate = createdDate;
    }
}
