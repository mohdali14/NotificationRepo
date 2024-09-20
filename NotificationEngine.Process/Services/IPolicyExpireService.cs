using NotificationEngine.Process.Data;

namespace NotificationEngine.Process.DBservices
{
    public interface IPolicyExpiryService
    {
        Task<List<Policy>> GetExpiringPoliciesForClient(int clientId);
    }
}