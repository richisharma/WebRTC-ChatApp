using Microsoft.EntityFrameworkCore;
using MessagingService.Data;
using MessagingService.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add Database Context
builder.Services.AddDbContext<MessagingDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add SignalR
builder.Services.AddSignalR();

// Add CQRS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", builder =>
    {
        builder.WithOrigins("http://localhost:3000") // Allow React app
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
    });
});

// Build App
var app = builder.Build();

// Run Data Seeder
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<MessagingDbContext>();
    var logger = services.GetRequiredService<ILogger<Program>>();

    try
    {
        DbInitializer.Seed(context, logger);
        logger.LogInformation("Database seeded successfully.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

app.UseCors("AllowReactApp");
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHub>("/chatHub");
});

app.Run();
