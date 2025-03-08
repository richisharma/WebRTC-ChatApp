using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MessagingService.Data;
using MessagingService.Models;

[Route("api/messages")]
[ApiController]
public class MessagesController : ControllerBase
{
    private readonly MessagingDbContext _context;
    private readonly IHubContext<ChatHub> _hubContext;

    public MessagesController(MessagingDbContext context, IHubContext<ChatHub> hubContext)
    {
        _context = context;
        _hubContext = hubContext;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendMessage([FromBody] ChatMessage model)
    {
        if (model == null)
            return BadRequest("Request body is missing.");

        Console.WriteLine($"Received Message: Sender={model.Sender}, Receiver={model.ReceiverId}, Message={model.Message}, RoomId={model.RoomId}");

        if (string.IsNullOrEmpty(model.Message))
            return BadRequest("Message cannot be empty.");

        if (string.IsNullOrEmpty(model.Sender) || string.IsNullOrEmpty(model.ReceiverId))
            return BadRequest("Sender and Receiver must be specified.");

        // If RoomId is present then it is a Group Chat
        if (!string.IsNullOrEmpty(model.RoomId))
        {
            Console.WriteLine($"Group Chat in Room: {model.RoomId}");
            await _hubContext.Clients.Group(model.RoomId)
                .SendAsync("ReceiveMessage", model.Sender, model.Message);
        }
        else
        {
            if (ChatHub._userConnections.TryGetValue(model.ReceiverId, out var connId))
            {
                await _hubContext.Clients.Client(connId)
                    .SendAsync("ReceiveMessage", model.Sender, model.Message);
            }
            else
            {
                Console.WriteLine($"No active connection for user {model.ReceiverId}");
            }
        }

        _context.ChatMessages.Add(model);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Message sent successfully!" });
    }

}
