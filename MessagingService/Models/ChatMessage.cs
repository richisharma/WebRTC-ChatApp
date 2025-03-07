using System.ComponentModel.DataAnnotations;

namespace MessagingService.Models
{
    public class ChatMessage
    {
        [Key]
        public int Id { get; set; }
        public string? RoomId { get; set; }  // Group ID
        public string Sender { get; set; }
        public string Message { get; set; }
        public string ReceiverId { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
