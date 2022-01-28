using System.Net;

namespace ProjectBlu.Dto;

public class Response<T>
{
    public T Resource { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    public Response(
        T resource,
        bool success,
        string message,
        HttpStatusCode statusCode
    )
    {
        Resource = resource;
        Success = success;
        Message = message;
        StatusCode = statusCode;
    }

    public Response(string message, HttpStatusCode statusCode)
    {
        Resource = default;
        Success = false;
        Message = message;
        StatusCode = statusCode;
    }

    public Response(T resource)
    {
        Resource = resource;

        if (resource == null)
        {
            Success = false;
            StatusCode = HttpStatusCode.NotFound;
            Message = "The requested resource can not be found.";
        }
        else
        {
            Success = true;
            StatusCode = HttpStatusCode.OK;
            Message = string.Empty;
        }
    }
}
