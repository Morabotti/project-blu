using Microsoft.AspNetCore.Mvc.Filters;

namespace ProjectBlu.Controllers.Filters;

public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
{
    public int Order { get; } = int.MaxValue - 10;

    public void OnActionExecuting(ActionExecutingContext context) { }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception != null)
        {
            switch (context.Exception)
            {
                case UnauthorizedAccessException exception:
                    context.Result = new ObjectResult(exception.Message)
                    {
                        StatusCode = 401,
                    };
                    context.ExceptionHandled = true;
                    break;

                case NotImplementedException exception:
                    context.Result = new ObjectResult(exception.Message)
                    {
                        StatusCode = 501,
                    };
                    context.ExceptionHandled = true;
                    break;

                default:
                    context.Result = new ObjectResult(context.Exception.Message)
                    {
                        StatusCode = 500,
                    };
                    context.ExceptionHandled = true;
                    break;
            }
        }
    }
}

