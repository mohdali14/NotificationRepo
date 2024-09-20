namespace NotificationEngine
{
    public interface IMessageHubClient
    {
        Task SendMessage(List<string> messages);

        Task SendMessageToClient(int clientId, List<string> message);

        Task AddToClientGroup(int clientId);

        Task RemoveFromClientGroup(int clientId);

    }
}