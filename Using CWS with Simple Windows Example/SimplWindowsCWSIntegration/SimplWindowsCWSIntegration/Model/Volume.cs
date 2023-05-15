using Newtonsoft.Json;
using SimplWindowsCWSIntegration.Converters;

namespace SimplWindowsCWSIntegration
{
    internal class Volume
    {
        /// <summary>
        /// The current volume level.
        /// </summary>
        [JsonProperty("level")]
        public ushort Level { get; set; }
        /// <summary>
        /// The volume mute status.
        /// </summary>
        [JsonProperty("mute")]
        [JsonConverter(typeof(JsonMuteBooleanConverter))]
        public bool Mute { get; set; }
    }
}
