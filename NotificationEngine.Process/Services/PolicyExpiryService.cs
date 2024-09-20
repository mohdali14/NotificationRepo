using Microsoft.EntityFrameworkCore;
using NotificationEngine.Process.Data;

namespace NotificationEngine.Process.DBservices
{
    public class PolicyExpiryService : IPolicyExpiryService
    {
        private readonly NotificationDbContext _dbContext;
        public PolicyExpiryService(NotificationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Policy>> GetExpiringPoliciesForClient(int clientId)
        {
            // Assuming you have a Policy entity with fields PolicyId, ClientId, and ExpiryDate
            DateTime expiryThreshold = DateTime.UtcNow.AddDays(15);

            var policies = await _dbContext.Policies
                                   .Where(p => p.ClientId == clientId && p.ExpirationDate >= DateTime.UtcNow && p.ExpirationDate <= expiryThreshold)
                                   .ToListAsync();

            return policies;
        }
    }
}
