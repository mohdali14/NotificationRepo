//using Microsoft.AspNetCore.SignalR;
//using Microsoft.Extensions.Hosting;

//namespace NotificationEngine
//{
//    public class PolicyNotificationService : IHostedService, IDisposable
//    {
//        private readonly IHubContext<PolicyNotificationHub> _hubContext;
//        private readonly string _connectionString;
//        private Timer _timer;

//        public PolicyNotificationService(IHubContext<PolicyNotificationHub> hubContext, string connectionString)
//        {
//            _hubContext = hubContext;
//            _connectionString = connectionString;
//        }


//        public Task StartAsync(CancellationToken cancellationToken)
//        {
//            _timer = new Timer(CheckExpiringPolicies, null, TimeSpan.Zero, TimeSpan.FromHours(1));  // Run every hour
//            return Task.CompletedTask;
//        }



//        private async void CheckExpiringPolicies(object state)
//        {
//            // Set up DbContext options with the provided connection string
//            var optionsBuilder = new DbContextOptionsBuilder<PolicyDbContext>();
//            optionsBuilder.UseSqlServer(_connectionString);

//            using (var dbContext = new YourDbContext(optionsBuilder.Options))
//            {
//                // Query policies expiring in the next 14 days
//                var expiringPolicies = await dbContext.Policies
//                    .Where(p => p.ExpirationDate >= DateTime.UtcNow && p.ExpirationDate <= DateTime.UtcNow.AddDays(14))
//                    .ToListAsync();

//                // Send notifications to all connected clients
//                foreach (var policy in expiringPolicies)
//                {
//                    var message = $"Policy {policy.PolicyId} is expiring on {policy.ExpirationDate}";
//                    await _hubContext.Clients.All.SendAsync("ReceiveNotification", message);
//                }
//            }
//        }

//        public Task StopAsync(CancellationToken cancellationToken)
//        {
//            _timer?.Change(Timeout.Infinite, 0);
//            return Task.CompletedTask;
//        }

//        public void Dispose()
//        {
//            _timer?.Dispose();
//        }
//    }
//}
