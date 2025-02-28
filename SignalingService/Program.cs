using SignalingService.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add SignalR service
builder.Services.AddSignalR();

var app = builder.Build();

// Enable WebSockets
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<SignalingHub>("/signalinghub"); // Define hub endpoint
});

app.Run();
