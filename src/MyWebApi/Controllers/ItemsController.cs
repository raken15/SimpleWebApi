using Microsoft.AspNetCore.Mvc;
using MyWebApi.Data;
using MyWebApi.Models;
using MyWebApi.ActionFilters;

namespace MyWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController: ControllerBase
{
    private readonly IItemRepository _itemRepository;
    public ItemsController(IItemRepository itemRepository) 
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
        try
        {
            var item = _itemRepository.GetItem(id);
            return Ok(item);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An unexpected error occurred. : " + ex.Message);
        }
    }

    [HttpPost]
    public IActionResult Post([FromBody] ItemRequestModel itemRequestModel)
    {
        try
        {
            _itemRepository.AddItem(itemRequestModel);
            var maxId = _itemRepository.GetMaxId();
            return CreatedAtRoute("GetItem", new { id = maxId }, itemRequestModel);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An unexpected error occurred. : " + ex.Message);
        }
    }

    [HttpPut("{id}")]
    [Item_IdMatchesRouteFilter]
    public IActionResult Put(int id, [FromBody] Item item)
    {
        try
        {
            var existingItem = _itemRepository.GetItem(id);
            _itemRepository.UpdateItem(item);
            return Ok(item);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An unexpected error occurred. : " + ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            var existingItem = _itemRepository.GetItem(id);
            _itemRepository.DeleteItem(id);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An unexpected error occurred. : " + ex.Message);
        }
        return NoContent();
    }
}
