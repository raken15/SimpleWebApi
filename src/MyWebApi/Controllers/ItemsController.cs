using Microsoft.AspNetCore.Mvc;
using MyWebApi.Data;
using MyWebApi.Models;
using MyWebApi.Filters.ActionFilters;
using MyWebApi.Filters.ExceptionFilters;

namespace MyWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController: ControllerBase
{
    private readonly IItemsRepository _itemRepository;
    public ItemsController(IItemsRepository itemRepository) 
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
    public IActionResult Post([FromBody] ItemRequestModel itemRequestModel)
    {
        _itemRepository.AddItem(itemRequestModel);
        var maxId = _itemRepository.GetMaxId();
        return CreatedAtRoute("GetItem", new { id = maxId }, itemRequestModel);
    }

    [HttpPut("{id}")]
    [Items_IdMatchesRouteFilter]
    public IActionResult Put(int id, [FromBody] Item item)
    {
        var existingItem = _itemRepository.GetItem(id);
        _itemRepository.UpdateItem(item);
        return Ok(item);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var existingItem = _itemRepository.GetItem(id);
        _itemRepository.DeleteItem(id);
        return NoContent();
    }
}
