using SignalingService.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add CQRS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", builder =>
    {
        builder.WithOrigins("http://localhost:3000") // Allow frontend
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
    });
});

// Add services to the container.
// Add SignalR service
builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true; // Show detailed SignalR errors
});

var app = builder.Build();

// Enable WebSockets
app.UseRouting();
app.UseCors("AllowReactApp");
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<SignalingHub>("/signalinghub"); // Define hub endpoint
});

app.Run();
