using Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiModule.Filters;

public class ControllerExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ControllerExceptionFilter> _logger;

    public ControllerExceptionFilter(ILogger<ControllerExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case EntityNotFoundException ex:
                SetResult(context, new ProblemDetails
                {
                    Title = "Not Found",
                    Detail = ex.Message,
                    Status = 404,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4"
                });
                break;
            case ArgumentException ex:
                SetResult(context, new ProblemDetails
                {
                    Title = "Bad Request",
                    Detail = ex.Message,
                    Status = 400,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
                });
                break;
            case Exception ex:
                SetResult(context, new ProblemDetails
                {
                    Title = "An error occured",
                    Detail = context.Exception.Message,
                    Status = 500,
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
                });
                break;
        }
    }

    private void SetResult(ExceptionContext context, ProblemDetails error)
    {
        _logger.LogError("Error in {ActionName}: {ErrorMessage}",
            context.ActionDescriptor.DisplayName, context.Exception.Message);
        context.Result = new ObjectResult(error);
        context.ExceptionHandled = true;
    }
}