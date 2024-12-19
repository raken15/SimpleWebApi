using System;
using Microsoft.AspNetCore.Mvc;

namespace MyWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController: ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Hello from MyWebApi");
    }

}
