using Microsoft.AspNetCore.SignalR;
using MSFSSimConnectWebAPI.Models;
using MSFSSimConnectWebAPI.Services;

namespace MSFSSimConnectWebAPI.Hubs
{
    /// <summary>
    /// SignalR Hub for real-time flight data streaming to connected clients (tablets, browsers)
    /// </summary>
    public class FlightDataHub : Hub
    {
        private readonly ISimConnectService _simConnectService;
        private readonly ILogger<FlightDataHub> _logger;

        public FlightDataHub(ISimConnectService simConnectService, ILogger<FlightDataHub> logger)
        {
            _simConnectService = simConnectService;
            _logger = logger;
        }

        /// <summary>
        /// Called when a client connects to the hub
        /// </summary>
        public override async Task OnConnectedAsync()
        {
            _logger.LogInformation($"Client connected: {Context.ConnectionId}");
            
            // Send current flight data immediately upon connection
            var flightData = await _simConnectService.GetFlightDataAsync();
            if (flightData != null)
            {
                await Clients.Caller.SendAsync("ReceiveFlightData", flightData);
            }

            await base.OnConnectedAsync();
        }

        /// <summary>
        /// Called when a client disconnects from the hub
        /// </summary>
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            _logger.LogInformation($"Client disconnected: {Context.ConnectionId}");
            return base.OnDisconnectedAsync(exception);
        }

        /// <summary>
        /// Client can request current flight data
        /// </summary>
        public async Task RequestFlightData()
        {
            var flightData = await _simConnectService.GetFlightDataAsync();
            await Clients.Caller.SendAsync("ReceiveFlightData", flightData);
        }

        /// <summary>
        /// Client can send a command to SimConnect
        /// </summary>
        public async Task SendCommand(SimConnectCommand command)
        {
            _logger.LogInformation($"Received command from client: {command.VariableName} = {command.Value}");
            var success = await _simConnectService.SendCommandAsync(command);
            await Clients.Caller.SendAsync("CommandResult", success);
        }
    }
}
