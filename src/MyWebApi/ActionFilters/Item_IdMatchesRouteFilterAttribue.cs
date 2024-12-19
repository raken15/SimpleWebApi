using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyWebApi.Models;


namespace MyWebApi.ActionFilters;

public class Item_IdMatchesRouteFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var bodyParameter = context.ActionArguments["item"] as Item;
        var idFromBody = bodyParameter?.Id;
        var routeId = context.HttpContext.Request.RouteValues["id"];
        if (!idFromBody.HasValue || idFromBody.ToString() != routeId?.ToString())
        {
            context.ModelState.AddModelError("id", "The id parameter does not match the id value in the route.");
            var problemDetails = new ValidationProblemDetails(context.ModelState)
            {
                Status = StatusCodes.Status400BadRequest,
            };
            context.Result = new BadRequestObjectResult(problemDetails);
        }
    }
}
