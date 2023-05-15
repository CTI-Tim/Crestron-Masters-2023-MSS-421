using Newtonsoft.Json;
using SimplWindowsCWSIntegration.Converters;

namespace SimplWindowsCWSIntegration
{
    /// <summary>
    /// The overall status of the system.
    /// </summary>
    internal class RoomStatus
    {
        /// <summary>
        /// Basic system information.
        /// </summary>
        [JsonProperty("system information")]
        public readonly SystemInfo SystemInfo = new SystemInfo();
        /// <summary>
        /// Basic program information.
        /// </summary>
        [JsonProperty("program information")]
        public readonly ProgramInfo ProgramInfo = new ProgramInfo();

        /// <summary>
        /// The curent power status (on / off)
        /// </summary>
        [JsonProperty("power")]
        [JsonConverter(typeof(JsonOnOffBooleanConverter))]
        public bool Power;

        /// <summary>
        /// The current video mute status (muted / unmuted).
        /// </summary>
        [JsonProperty("video mute")]
        [JsonConverter(typeof(JsonMuteBooleanConverter))]
        public bool VideoMute;

        /// <summary>
        /// The current volume status (level - mute).
        /// </summary>
        [JsonProperty("volume")]
        public readonly Volume Volume = new Volume();

        /// <summary>
        /// The current lighting status ( on / off - scene - level).
        /// </summary>
        [JsonProperty("lights")]
        public readonly Lighting Lights = new Lighting();

        /// <summary>
        /// The current shade position (open / closed).
        /// </summary>
        [JsonProperty("shades")]
        [JsonConverter(typeof(JsonShadesBooleanConverter))]
        public bool Shades;

        /// <summary>
        /// The current source.
        /// </summary>
        [JsonProperty("source")]
        public string Source = "none";

        /// <summary>
        /// The current system status.
        /// </summary>
        [JsonProperty("status")]
        public string Status = "unknown";

        /// <summary>
        /// For example only, a non-persistant log to show how errors could be relayed to the CWS service.
        /// </summary>
        [JsonProperty("error log")]
        public readonly FixedSizedQueue<Error> LogEntries = new FixedSizedQueue<Error>(50);
    }
}
