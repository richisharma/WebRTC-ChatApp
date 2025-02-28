using MessagingService.Models;
using Microsoft.EntityFrameworkCore;

namespace MessagingService.Data
{
    public class MessagingDbContext(DbContextOptions options): DbContext(options)
    {
        public DbSet<ChatMessage> ChatMessages { get; set; }
    }
}
