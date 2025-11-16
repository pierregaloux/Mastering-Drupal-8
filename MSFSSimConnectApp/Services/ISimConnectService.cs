using MSFSSimConnectWebAPI.Models;

namespace MSFSSimConnectWebAPI.Services
{
    /// <summary>
    /// Interface for SimConnect service
    /// </summary>
    public interface ISimConnectService
    {
        /// <summary>
        /// Connect to MSFS SimConnect
        /// </summary>
        Task<bool> ConnectAsync();

        /// <summary>
        /// Disconnect from MSFS SimConnect
        /// </summary>
        Task DisconnectAsync();

        /// <summary>
        /// Check if connected to SimConnect
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// Get current flight data
        /// </summary>
        Task<FlightData?> GetFlightDataAsync();

        /// <summary>
        /// Send a command to SimConnect
        /// </summary>
        Task<bool> SendCommandAsync(SimConnectCommand command);

        /// <summary>
        /// Event that fires when new flight data is available
        /// </summary>
        event EventHandler<FlightData>? FlightDataUpdated;
    }
}
