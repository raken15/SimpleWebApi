using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyWebApi.Filters.ActionFilters;

public class LogActionFilter : ActionFilterAttribute
{
    private readonly ILogger<LogActionFilter> _logger;

    public LogActionFilter(ILogger<LogActionFilter> logger)
    {
        _logger = logger;
    }
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        Log("OnActionExecuting", context);
        base.OnActionExecuting(context);
    }
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        Log("OnActionExecuted", context);
        base.OnActionExecuted(context);
    }
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        Log("OnResultExecuting", context);
        base.OnResultExecuting(context);
    }

    public override void OnResultExecuted(ResultExecutedContext context)
    {
        Log("OnResultExecuted", context);
        base.OnResultExecuted(context);
    }
    public void Log(string methodName, FilterContext context)
    {
        var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
        string controllerName = controllerActionDescriptor?.ControllerName ?? "UnknownController";
        string actionName = controllerActionDescriptor?.ActionName ?? "UnknownAction";

        _logger.LogInformation($"[{methodName}] Controller: {controllerName}, Action: {actionName}");
    }

}
