using Microsoft.AspNetCore.SignalR;

namespace MessagingService.Hubs
{
    public class ChatHub : Hub
    {
        // Send a message to all clients in a room
        public async Task SendMessage(string roomId, string sender, string message)
        {
            await Clients.Group(roomId).SendAsync("ReceiveMessage", sender, message, DateTime.UtcNow);
        }

        // Join a chat room
        public async Task JoinRoom(string roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        }

        // Leave a chat room
        public async Task LeaveRoom(string roomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
        }
    }
}
