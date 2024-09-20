//using Microsoft.AspNetCore.SignalR;

//namespace NotificationEngine
//{
//    public class PolicyNotificationHub : Hub
//    {
//        public async Task JoinClientGroup(string clientId)
//        {
//            // Add the connected user to a group identified by the clientId
//            await Groups.AddToGroupAsync(Context.ConnectionId, clientId);
//        }

//        public async Task LeaveClientGroup(string clientId)
//        {
//            // Remove the connected user from the group
//            await Groups.RemoveFromGroupAsync(Context.ConnectionId, clientId);
//        }
//        public async Task SendNotification(string message)
//        {
//            await Client.All.SendAsync("ReceiveNotification", message);
//        }

//        //public async Task SendMessage(string message)
//        //{
//        //    await Client.All.SendAsync("ReceiveNotification", message);
//        //}
//    }
//}
