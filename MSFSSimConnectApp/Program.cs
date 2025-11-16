using MSFSSimConnectWebAPI.Services;
using MSFSSimConnectWebAPI.Hubs;
using MSFSSimConnectWebAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS policy to allow access from tablets/other devices
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add SignalR for real-time communication
builder.Services.AddSignalR();

// Register SimConnect service as a singleton (one instance for the app)
builder.Services.AddSingleton<ISimConnectService, SimConnectService>();

// Add the background service for broadcasting flight data
builder.Services.AddHostedService<FlightDataBroadcastService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS
app.UseCors("AllowAll");

// Serve static files (HTML/JS/CSS) from wwwroot
app.UseStaticFiles();

// Map SignalR hub
app.MapHub<FlightDataHub>("/flightDataHub");

// API Endpoints

// Get connection status
app.MapGet("/api/connection/status", (ISimConnectService simConnect) =>
{
    return Results.Ok(new { IsConnected = simConnect.IsConnected });
})
.WithName("GetConnectionStatus")
.WithOpenApi();

// Connect to SimConnect
app.MapPost("/api/connection/connect", async (ISimConnectService simConnect) =>
{
    var success = await simConnect.ConnectAsync();
    return success 
        ? Results.Ok(new { Message = "Connected successfully" }) 
        : Results.Problem("Failed to connect to SimConnect");
})
.WithName("Connect")
.WithOpenApi();

// Disconnect from SimConnect
app.MapPost("/api/connection/disconnect", async (ISimConnectService simConnect) =>
{
    await simConnect.DisconnectAsync();
    return Results.Ok(new { Message = "Disconnected successfully" });
})
.WithName("Disconnect")
.WithOpenApi();

// Get current flight data
app.MapGet("/api/flight/data", async (ISimConnectService simConnect) =>
{
    var data = await simConnect.GetFlightDataAsync();
    return data != null 
        ? Results.Ok(data) 
        : Results.NotFound(new { Message = "No flight data available" });
})
.WithName("GetFlightData")
.WithOpenApi();

// Send a command to SimConnect
app.MapPost("/api/flight/command", async (SimConnectCommand command, ISimConnectService simConnect) =>
{
    var success = await simConnect.SendCommandAsync(command);
    return success 
        ? Results.Ok(new { Message = "Command sent successfully" }) 
        : Results.Problem("Failed to send command");
})
.WithName("SendCommand")
.WithOpenApi();

app.Run();
