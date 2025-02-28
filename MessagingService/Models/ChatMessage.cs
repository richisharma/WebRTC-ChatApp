using System.ComponentModel.DataAnnotations;

namespace MessagingService.Models
{
    public class ChatMessage
    {
        [Key]
        public int Id { get; set; }
        public required string RoomId { get; set; }  // Group ID
        public required string Sender { get; set; }
        public required string Message { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
