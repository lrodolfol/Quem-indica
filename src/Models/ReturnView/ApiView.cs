namespace API.Models.ReturnView;

public sealed class ApiView
{
    public List<string> Message { get; private set; } = [];
    public bool Success { get; private set; } = true;
    public ushort Code { get; private set; } = 200;

    public void SetMessage(string message) => Message.Add(message);
    public void SetMessage(List<string> messages) => Message.AddRange(messages);
    public void SetSuccess() => Success = true;
    public void SetUnSuccess() => Success = false;
    public void SetCode(ushort code) => Code = code;
    public void SetValues(List<string> message, ushort code, bool success)
    {
        Message.AddRange(message);
        Code = code;
        Success = success;
    }
}
