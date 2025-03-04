using MessagingService.Data;
using MessagingService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MessagingService.Controllers
{
    [Route("api/messages")]
    [ApiController]
    public class MessagesController: ControllerBase
    {
        private readonly MessagingDbContext _context;

        public MessagesController(MessagingDbContext context)
        {
            _context = context;
        }

        // Get chat history by roomId
        [HttpGet("{roomId}")]
        public async Task<ActionResult<IEnumerable<ChatMessage>>> GetMessages(string roomId)
        {
            var messages = await _context.ChatMessages
                .Where(m => m.RoomId == roomId)
                .OrderBy(m => m.Timestamp)
                .ToListAsync();
            return Ok(messages);
        }

        //send message
        [HttpPost("send")] 
        public async Task<IActionResult> SendMessage([FromBody] ChatMessage model)
        {
            if (string.IsNullOrEmpty(model.Message))
                return BadRequest("Message cannot be empty.");

            // Save message to database
            _context.ChatMessages.Add(model);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Message sent successfully!" });
        }

    }
}
