using System;
using System.ComponentModel.DataAnnotations;

namespace MyWebApi.Models;

public class ItemRequestModel
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Range(1, 60)]
    public decimal Price { get; set; }
    public DateTime CreatedDate { get; set; }

    public ItemRequestModel(string name, decimal price, DateTime createdDate)
    {
        Name = name;
        Price = price;
        CreatedDate = createdDate;
    }
}
