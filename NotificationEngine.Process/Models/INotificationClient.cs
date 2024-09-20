namespace NotificationEngine.Process.Models
{
    public interface INotificationClient
    {
        string GetClientId();
        void SetClientId(string clientId);
    }
}
