using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Security.Claims;

public class ChatHub : Hub
{
    // Dictionary to store userId → connectionId
    public static ConcurrentDictionary<string, string> _userConnections = new ConcurrentDictionary<string, string>();

    public override async Task OnConnectedAsync()
    {
        // Log all JWT claims from the connected user
        if (Context.User != null)
        {
            foreach (var claim in Context.User.Claims)
            {
                Console.WriteLine($"{claim.Type}: {claim.Value}");
            }
        }
        else
        {
            Console.WriteLine("No user claims found.");
        }
        // Extract user ID from token claims (e.g., "NameIdentifier")
        var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!string.IsNullOrEmpty(userId))
        {
            _userConnections[userId] = Context.ConnectionId;
            Console.WriteLine($"OnConnectedAsync: {userId} => {Context.ConnectionId}");
        }
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        // Find the user ID whose ConnectionId matches Context.ConnectionId
        var userEntry = _userConnections.FirstOrDefault(x => x.Value == Context.ConnectionId);
        if (!string.IsNullOrEmpty(userEntry.Key))
        {
            _userConnections.TryRemove(userEntry.Key, out _);
            Console.WriteLine($"OnDisconnectedAsync: {userEntry.Key} => Removed");
        }
        await base.OnDisconnectedAsync(exception);
    }

    // A direct method to send messages from hub context
    public async Task SendMessage(string receiverId, string message)
    {
        if (_userConnections.TryGetValue(receiverId, out var connId))
        {
            await Clients.Client(connId).SendAsync("ReceiveMessage", Context.ConnectionId, message);
        }
    }
    // Called when a client wants to join a chat room
    public async Task JoinRoom(string roomId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        await Clients.Group(roomId).SendAsync("ReceiveSystemMessage", $"User {Context.ConnectionId} has joined the room.");
    }

    // Called when a client wants to leave a chat room
    public async Task LeaveRoom(string roomId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
        await Clients.Group(roomId).SendAsync("ReceiveSystemMessage", $"User {Context.ConnectionId} has left the room.");
    }

    // Send a message to everyone in a room
    public async Task SendRoomMessage(string roomId, string sender, string message)
    {
        await Clients.Group(roomId).SendAsync("ReceiveMessage", sender, message, roomId);
    }
}