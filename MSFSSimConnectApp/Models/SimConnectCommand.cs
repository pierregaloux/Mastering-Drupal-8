namespace MSFSSimConnectWebAPI.Models
{
    /// <summary>
    /// Model representing a command to send to SimConnect
    /// </summary>
    public class SimConnectCommand
    {
        /// <summary>
        /// The variable name to set (e.g., "AUTOPILOT_MASTER", "HEADING_BUG_SET")
        /// </summary>
        public string VariableName { get; set; } = string.Empty;

        /// <summary>
        /// The value to set
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Unit type (e.g., "Bool", "Degrees", "Number")
        /// </summary>
        public string Unit { get; set; } = "Number";
    }
}
