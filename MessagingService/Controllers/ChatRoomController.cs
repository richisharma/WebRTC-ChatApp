using Microsoft.AspNetCore.Mvc;
using MessagingService.Data;
using MessagingService.Models;
using System.Threading.Tasks;
using System.Linq;

namespace MessagingService.Controllers
{
    [Route("api/chatrooms")]
    [ApiController]
    public class ChatRoomsController : ControllerBase
    {
        private readonly MessagingDbContext _context;

        public ChatRoomsController(MessagingDbContext context)
        {
            _context = context;
        }

        // POST: api/chatrooms/create
        [HttpPost("create")]
        public async Task<IActionResult> CreateRoom([FromBody] ChatRoom model)
        {
            if (model == null || string.IsNullOrEmpty(model.RoomName))
            {
                return BadRequest("Room name is required.");
            }

            _context.ChatRooms.Add(model);
            await _context.SaveChangesAsync();

            return Ok(model);
        }

        // GET: api/chatrooms
        [HttpGet]
        public IActionResult GetRooms()
        {
            var rooms = _context.ChatRooms.ToList();
            return Ok(rooms);
        }
    }
}
