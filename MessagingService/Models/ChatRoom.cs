using System.ComponentModel.DataAnnotations;

namespace MessagingService.Models
{
    public class ChatRoom
    {
        [Key]
        public Guid RoomId { get; set; } = Guid.NewGuid();

        [Required]
        public string RoomName { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
