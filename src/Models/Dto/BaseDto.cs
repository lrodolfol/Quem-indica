namespace API.Models.Dto;

public abstract class BaseDto
{
    private readonly List<string> _notifications = [];
    public IReadOnlyCollection<string> Notifications => _notifications;
    protected void AddNotifications(string notification) =>
        _notifications.Add(notification);
    protected void AddNotifications(string[] notification) =>
        _notifications.AddRange(notification);
    public abstract bool Validate();
}
