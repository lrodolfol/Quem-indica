using System.Net;

namespace API.Models.ReturnView;

public sealed class ApiView
{
    public List<string> Message { get; private set; } = [];
    public bool Success { get; private set; } = true;
    public HttpStatusCode HttpStatusCode { get; private set; } = HttpStatusCode.OK;
    public object Data { get; private set; } = new { };

    public void SetMessage(string message) => Message.Add(message);
    public void SetMessage(List<string> messages) => Message.AddRange(messages);
    public void SetSuccess() => Success = true;
    public void SetUnSuccess() => Success = false;
    public void SetCode(HttpStatusCode httpStatusCode) => HttpStatusCode = httpStatusCode;
    public void SetData(object data) => Data = data;
    public void SetValues(List<string> message, HttpStatusCode httpStatusCode, bool success, object? data = null)
    {
        Message.AddRange(message);
        HttpStatusCode = httpStatusCode;
        Success = success;
        Data = data ?? Data;
    }
    public void SetValues(string message, HttpStatusCode httpStatusCode, bool success, object? data = null)
    {
        Message.Add(message);
        HttpStatusCode = httpStatusCode;
        Success = success;
        Data = data ?? Data;
    }
}
