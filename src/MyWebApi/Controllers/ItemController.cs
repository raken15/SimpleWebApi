using Microsoft.AspNetCore.Mvc;
using MyWebApi.Data;
using MyWebApi.Models;
using MyWebApi.Filters.ActionFilters;

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
        _itemRepository.AddItem(item);
        var maxId = _itemRepository.GetMaxId();
        return CreatedAtRoute("GetItem", new { id = maxId }, item);
    }

    [HttpPut]
    [Item_IdMatchesRouteFilter]
    public IActionResult Put([FromBody] Item item)
    {
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
