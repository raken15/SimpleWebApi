using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyWebApi.Filters.ExceptionFilters;

public class Items_ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is KeyNotFoundException keyNotFoundException)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Not Found",
                Detail = keyNotFoundException.Message,
                Status = StatusCodes.Status404NotFound
            };
            context.Result = new NotFoundObjectResult(problemDetails);
            context.ExceptionHandled = true;
        }
        else if (context.Exception is ArgumentException argumentException)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Bad Request",
                Detail = argumentException.Message,
                Status = StatusCodes.Status400BadRequest
            };
            context.Result = new BadRequestObjectResult(problemDetails);
            context.ExceptionHandled = true;
        }
        else if (context.Exception is ArgumentNullException argumentNullException)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Bad Request",
                Detail = argumentNullException.Message,
                Status = StatusCodes.Status400BadRequest
            };
            context.Result = new BadRequestObjectResult(problemDetails);
            context.ExceptionHandled = true;
        }
        else if (context.Exception is InvalidOperationException invalidOperationException)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Invalid Operation",
                Detail = invalidOperationException.Message,
                Status = StatusCodes.Status400BadRequest
            };
            context.Result = new BadRequestObjectResult(problemDetails);
            context.ExceptionHandled = true;
        }
        else if (context.Exception != null)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Server Error",
                Detail = "An unexpected error occurred. : " + context.Exception.Message,
                Status = StatusCodes.Status500InternalServerError
            };
            context.Result = new ObjectResult(problemDetails)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
            context.ExceptionHandled = true;
        }
    }
}
