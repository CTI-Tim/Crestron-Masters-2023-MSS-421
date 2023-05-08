using Newtonsoft.Json;
using SimplWindowsCWSIntegration.Converters;

namespace SimplWindowsCWSIntegration
{
    internal class Volume
    {
        [JsonProperty("level")]
        public ushort Level { get; set; }

        [JsonProperty("mute")]
        [JsonConverter(typeof(JsonMuteBooleanConverter))]
        public bool Mute { get; set; }
    }
}
