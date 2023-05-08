using Newtonsoft.Json;
using SimplWindowsCWSIntegration.Converters;

namespace SimplWindowsCWSIntegration
{
    internal class RoomStatus
    {

        [JsonProperty("system information")]
        public readonly SystemInfo SystemInfo = new SystemInfo();

        [JsonProperty("program information")]
        public readonly ProgramInfo ProgramInfo = new ProgramInfo();

        [JsonProperty("power")]
        [JsonConverter(typeof(JsonOnOffBooleanConverter))]
        public bool Power;

        [JsonProperty("video mute")]
        [JsonConverter(typeof(JsonMuteBooleanConverter))]
        public bool VideoMute;

        [JsonProperty("volume")]
        public readonly Volume Volume = new Volume();

        [JsonProperty("lights")]
        public readonly Lighting Lights = new Lighting();

        [JsonProperty("shades")]
        [JsonConverter(typeof(JsonShadesBooleanConverter))]
        public bool Shades;

        [JsonProperty("source")]
        public string Source = "none";

        [JsonProperty("status")]
        public string Status = "unknown";

        [JsonProperty("error log")]
        public readonly FixedSizedQueue<Error> LogEntries = new FixedSizedQueue<Error>(50);

    }
}
