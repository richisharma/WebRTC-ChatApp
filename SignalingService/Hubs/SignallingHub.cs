using Microsoft.AspNetCore.SignalR;

namespace SignalingService.Hubs
{
    public class SignallingHub : Hub
    {
        // Send SDP Offer/Answer
        public async Task SendSignal(string receiverConnectionId, string message)
        {
            await Clients.Client(receiverConnectionId).SendAsync("ReceiveSignal", Context.ConnectionId, message);
        }

        // Send ICE Candidates
        public async Task SendIceCandidate(string receiverConnectionId, string candidate)
        {
            await Clients.Client(receiverConnectionId).SendAsync("ReceiveIceCandidate", Context.ConnectionId, candidate);
        }

        // Notify when a user joins
        public async Task JoinRoom(string roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        }

        // Notify when a user leaves
        public async Task LeaveRoom(string roomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
        }
    }
}
