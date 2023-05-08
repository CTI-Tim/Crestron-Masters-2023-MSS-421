using Newtonsoft.Json;
using SimplWindowsCWSIntegration.Converters;

namespace SimplWindowsCWSIntegration
{
    internal class Lighting
    {
        [JsonProperty("state")]
        [JsonConverter(typeof(JsonOnOffBooleanConverter))]
        public bool State;

        [JsonProperty("scene")]
        public ushort Scene;

        [JsonProperty("level")]
        public ushort Level;
    }
}
