using Microsoft.AspNetCore.SignalR;

namespace SignalingService.Hubs
{
    public class SignalingHub : Hub
    {

        public async Task<string> GetConnectionId()
        {
            return Context.ConnectionId;
        }

        // Send SDP Offer/Answer
        public async Task SendSignal(string receiverConnectionId, string message)
        {
            await Clients.Client(receiverConnectionId).SendAsync("ReceiveSignal", Context.ConnectionId, message);
        }

        // Send ICE Candidates
        public async Task SendIceCandidate(string receiverConnectionId, string candidate)
        {
            Console.WriteLine($"Received ICE Candidate from {Context.ConnectionId} → Forwarding to {receiverConnectionId}");
            await Clients.Client(receiverConnectionId).SendAsync("ReceiveIceCandidate", Context.ConnectionId, candidate);
        }
    }
}
