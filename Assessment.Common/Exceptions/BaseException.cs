using System.Net;

namespace Assessment.Common.Exceptions;

public class BaseException (
    string message, 
    string title, 
    HttpStatusCode statusCode = HttpStatusCode.InternalServerError 
    ) : Exception(message)
{
    public string? Title { get; set; } = title;
    public HttpStatusCode StatusCode { get; set; } = statusCode;
}