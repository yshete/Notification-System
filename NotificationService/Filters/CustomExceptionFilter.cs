using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace NotificationService.Filters;

public class CustomExceptionFilter : ExceptionFilterAttribute
{
    private readonly ILogger<CustomExceptionFilter> _logger;
    
    public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
    {
        _logger = logger;
    }

    public override async Task OnExceptionAsync(ExceptionContext context)
    {
        var actionDescriptor = context.ActionDescriptor;
        var controllerName = actionDescriptor.RouteValues["controller"];
        var actionName = actionDescriptor.RouteValues["action"];

        // Log exception with controller and action information
        _logger.LogError(context.Exception, $"Exception occurred in controller '{controllerName}', action '{actionName}'");

        var result = new ObjectResult(new
        {
            Message = "An unexpected error occurred. Please try again later.",
            Detailed = context.Exception.Message // Optional
        })
        {
            StatusCode = 500 // Internal Server Error
        };

        context.Result = result;
        context.ExceptionHandled = true;
        
        await Task.CompletedTask; // Ensure method is async-compliant
    }
}
