using Microsoft.AspNetCore.SignalR;
using NotificationEngine.Process.DBservices;
using NotificationEngine.Process.Models;

namespace NotificationEngine.Process.AllHubs
{
    public class PolicyHub : Hub
    {
        private readonly INotificationClient _notificationClient;
        private readonly IPolicyExpiryService _PolicyExpiryService;

        public PolicyHub(INotificationClient notificationClient, IPolicyExpiryService PolicyExpiryService)
        {
            _notificationClient = notificationClient;
            _PolicyExpiryService = PolicyExpiryService;

        }

        // Add client group management 
        public async Task JoinClientGroup(string clientId)
        {
            _notificationClient.SetClientId(clientId);
            await Groups.AddToGroupAsync(Context.ConnectionId, clientId);

            await SendPolicyUpdate(clientId);
        }

        public async Task SendPolicyUpdate(string clientId)
        {
            var expiringPolicies = await _PolicyExpiryService.GetExpiringPoliciesForClient(int.Parse(clientId));

            foreach (var policy in expiringPolicies)
            {
                var notification = new Notification();
                notification.policyId = policy.PolicyId;
                notification.Message = $"Policy Number '{policy.Number}' is expiring soon!";
                notification.CreatedAt = DateTime.Now.ToUniversalTime();
                notification.IsRead = true;

                await Clients.Group(clientId).SendAsync("ReceiveClientUpdates", notification);
            }
        }

        public override async Task OnConnectedAsync()
        {
            string connectionId = Context.ConnectionId;
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            // Optionally, remove the user from their client group when they disconnect
            var clientId = _notificationClient.GetClientId(); // Assuming the clientId is already stored
            if (!string.IsNullOrEmpty(clientId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, clientId);
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
