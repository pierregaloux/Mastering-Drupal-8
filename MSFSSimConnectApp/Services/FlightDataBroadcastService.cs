using Microsoft.AspNetCore.SignalR;
using MSFSSimConnectWebAPI.Hubs;
using MSFSSimConnectWebAPI.Services;

namespace MSFSSimConnectWebAPI.Services
{
    /// <summary>
    /// Background service that broadcasts flight data updates to all connected clients
    /// </summary>
    public class FlightDataBroadcastService : BackgroundService
    {
        private readonly ISimConnectService _simConnectService;
        private readonly IHubContext<FlightDataHub> _hubContext;
        private readonly ILogger<FlightDataBroadcastService> _logger;

        public FlightDataBroadcastService(
            ISimConnectService simConnectService,
            IHubContext<FlightDataHub> hubContext,
            ILogger<FlightDataBroadcastService> logger)
        {
            _simConnectService = simConnectService;
            _hubContext = hubContext;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Flight Data Broadcast Service starting...");

            // Subscribe to flight data updates
            _simConnectService.FlightDataUpdated += async (sender, flightData) =>
            {
                try
                {
                    // Broadcast to all connected clients
                    await _hubContext.Clients.All.SendAsync("ReceiveFlightData", flightData, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error broadcasting flight data");
                }
            };

            // Attempt to connect to SimConnect
            var connected = await _simConnectService.ConnectAsync();
            if (!connected)
            {
                _logger.LogWarning("Could not connect to SimConnect on startup. Running in demo mode.");
            }

            // Keep the service running
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }

            _logger.LogInformation("Flight Data Broadcast Service stopping...");
            await _simConnectService.DisconnectAsync();
        }
    }
}
