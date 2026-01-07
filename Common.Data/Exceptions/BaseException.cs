using System.Net;

namespace Common.Data.Exceptions;

public class BaseException (
    string message, 
    string title, 
    HttpStatusCode statusCode = HttpStatusCode.InternalServerError 
    ) : Exception(message)
{
    public string? Title { get; set; } = title;
    public HttpStatusCode StatusCode { get; set; } = statusCode;
}
