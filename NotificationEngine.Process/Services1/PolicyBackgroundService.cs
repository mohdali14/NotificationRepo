using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using NotificationEngine.Process.AllHubs;
using NotificationEngine.Process.DBservices;
using NotificationEngine.Process.Models;

namespace NotificationEngine.Process.Services
{
    public class PolicyBackgroundService : BackgroundService
    {
        private readonly IHubContext<PolicyHub> _hubContext;
        private readonly IHostApplicationLifetime _lifetime;
        private readonly INotificationClient _NotificationClient;
        private readonly IPolicyExpiryService _policyExpiryService;

        public PolicyBackgroundService(IHubContext<PolicyHub> hubContext, IHostApplicationLifetime lifetime, INotificationClient NotificationClient
            , IPolicyExpiryService policyExpiryService)
        {
            _hubContext = hubContext;
            _lifetime = lifetime;
            _NotificationClient = NotificationClient;
            _policyExpiryService = policyExpiryService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Ensure that the SignalR pipeline is fully initialized before starting
            await Task.Delay(5000, stoppingToken); // Delay to ensure everything is up

            //string clientId = "550224";
            while (!stoppingToken.IsCancellationRequested)
            {
                // Get the ClientId from the store
                var clientId = _NotificationClient.GetClientId();
                // Simulate fetching expiring policies from the database
                var expiringPolicies = await _policyExpiryService.GetExpiringPoliciesForClient(int.Parse(clientId));

                foreach (var policy in expiringPolicies)
                {
                    var notification = new Notification();
                    notification.policyId = policy.PolicyId;
                    notification.Message = $"Policy Number '{policy.Number}' is expiring soon!";
                    notification.CreatedAt = DateTime.Now.ToUniversalTime();
                    notification.IsRead = true;

                    // Notify all clients in the group based on clientId
                    //await _hubContext.Clients.Group(policy.ClientId)
                    //  .SendAsync("ReceiveClientUpdates", $"Policy '{policy.PolicyName}' is expiring soon!");

                    await _hubContext.Clients.Group(clientId).SendAsync("ReceiveClientUpdates", notification);
                }

                // Wait for 30 minutes before checking again
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }


        }
    }
}
