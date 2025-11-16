namespace MSFSSimConnectWebAPI.Models
{
    /// <summary>
    /// Model representing flight data from MSFS
    /// </summary>
    public class FlightData
    {
        /// <summary>
        /// Aircraft title/name
        /// </summary>
        public string AircraftTitle { get; set; } = string.Empty;

        /// <summary>
        /// Latitude in degrees
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Longitude in degrees
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Altitude in feet
        /// </summary>
        public double Altitude { get; set; }

        /// <summary>
        /// Indicated airspeed in knots
        /// </summary>
        public double IndicatedAirspeed { get; set; }

        /// <summary>
        /// Ground speed in knots
        /// </summary>
        public double GroundSpeed { get; set; }

        /// <summary>
        /// Heading in degrees
        /// </summary>
        public double Heading { get; set; }

        /// <summary>
        /// Vertical speed in feet per minute
        /// </summary>
        public double VerticalSpeed { get; set; }

        /// <summary>
        /// Pitch angle in degrees
        /// </summary>
        public double Pitch { get; set; }

        /// <summary>
        /// Bank angle in degrees
        /// </summary>
        public double Bank { get; set; }

        /// <summary>
        /// Whether the aircraft is on the ground
        /// </summary>
        public bool OnGround { get; set; }

        /// <summary>
        /// Timestamp when data was captured
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
