using Microsoft.AspNetCore.SignalR;

namespace NotificationEngine
{
    public class MessageHub : Hub<IMessageHubClient>
    {
        public async Task SendMessage(List<string> message)
        {
            await Clients.All.SendMessage(message);
        }

        public async Task SendMessageToClient(int clientId, List<string> message)
        {
            // Send message to the group corresponding to the specified clientId
            await Clients.Group(clientId.ToString()).SendMessage(message);
        }

        // When a user connects, add them to the client-specific group
        public async Task AddToClientGroup(int clientId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, clientId.ToString());
        }

        // Optionally, when a user disconnects, remove them from the client-specific group
        public async Task RemoveFromClientGroup(int clientId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, clientId.ToString());
        }
    }
}
