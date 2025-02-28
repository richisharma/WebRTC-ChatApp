using MessagingService.Models;

namespace MessagingService.Data
{
    public class DbInitializer
    {
        public static void Seed(MessagingDbContext context, ILogger logger)
        {
            // Ensure the database is created
            context.Database.EnsureCreated();
            logger.LogInformation("Seeding data started");

            // Check if any messages already exist
            if (context.ChatMessages.Any())
            {
                logger.LogInformation("database already seeded");
                return; // DB has been seeded already
            }

            var messages = new ChatMessage[]
            {
                new ChatMessage { RoomId = "general", Sender = "Alice", Message = "Hello, everyone!", Timestamp = DateTime.UtcNow },
                new ChatMessage { RoomId = "general", Sender = "Bob", Message = "Hi Alice! How are you?", Timestamp = DateTime.UtcNow },
                new ChatMessage { RoomId = "tech", Sender = "Charlie", Message = "Is anyone here into WebRTC?", Timestamp = DateTime.UtcNow }
            };

            context.ChatMessages.AddRange(messages);
            context.SaveChanges();

            logger.LogInformation("Database seeded successfully");
        }
    }
}
