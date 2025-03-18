using Microsoft.AspNetCore.Mvc;
using MyWebApi.Data;
using MyWebApi.Models;

namespace MyWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemController: ControllerBase
{
    private readonly IItemRepository _itemRepository;
    public ItemController(IItemRepository itemRepository) 
    {
        _itemRepository = itemRepository;
    }
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_itemRepository.GetAllItems());
    }

    [HttpGet("{id}", Name = "GetItem")]
    public IActionResult GetItem(int id)
    {
        var item = _itemRepository.GetItem(id);
        return Ok(item);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Item item)
    {
        var newItem = _itemRepository.AddItem(item);
        return CreatedAtRoute("GetItem", new { id = newItem.Id }, newItem);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Item item)
    {
        item.Id = id;
        _itemRepository.UpdateItem(item);
        return Ok(item);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _itemRepository.DeleteItem(id);
        return NoContent();
    }
}
