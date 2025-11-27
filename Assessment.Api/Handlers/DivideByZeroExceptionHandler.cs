using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

public class DivideByZeroExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
      HttpContext httpContext, 
      Exception exception, 
      CancellationToken cancellationToken)
    {
        if(exception is not DivideByZeroException zeroException)
        {
            return true;
        }

        var problemDetails = new ProblemDetails
        {
            Title = "An error occurred",
            Status = StatusCodes.Status400BadRequest,
            Detail = exception.Message
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}