

using NotificationEngine.Process.Models;

namespace NotificationMicroservice.Models
{
    public class NotificationClient : INotificationClient
    {
        private string _clientId;

        public string GetClientId()
        {
            return _clientId;
        }

        public void SetClientId(string clientId)
        {
            _clientId = clientId;
        }
    }
}
