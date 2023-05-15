using Newtonsoft.Json;
using SimplWindowsCWSIntegration.Converters;

namespace SimplWindowsCWSIntegration
{
    internal class Lighting
    {
        /// <summary>
        /// The state of the light (on / off).
        /// </summary>
        [JsonProperty("state")]
        [JsonConverter(typeof(JsonOnOffBooleanConverter))]
        public bool State;
        /// <summary>
        /// The current lighting scene.
        /// </summary>
        [JsonProperty("scene")]
        public ushort Scene;
        /// <summary>
        /// The current lighting level.
        /// </summary>
        [JsonProperty("level")]
        public ushort Level;
    }
}
