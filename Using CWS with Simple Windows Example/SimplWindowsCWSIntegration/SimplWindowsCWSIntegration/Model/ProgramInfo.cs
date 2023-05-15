using Crestron.SimplSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SimplWindowsCWSIntegration
{
    /// <summary>
    /// Serialisable class encapsulating basic information about the running program.
    /// </summary>
    internal class ProgramInfo
    {   

        [JsonProperty("compatability")]
        [JsonConverter(typeof(StringEnumConverter))]
        public eCrestronSeries Compatability
        {
            get => CrestronEnvironment.ProgramCompatibility;
        }

        [JsonProperty("runtime environment")]
        [JsonConverter(typeof(StringEnumConverter))]
        public eRuntimeEnvironment RuntimeEnvironment
        {
            get => CrestronEnvironment.RuntimeEnvironment;
        }

        [JsonProperty("room name")]
        public string RoomName
        {
            get => InitialParametersClass.RoomName;
        }

        [JsonProperty("room id")]
        public string RoomId
        {
            get => InitialParametersClass.RoomId;
        }

        [JsonProperty("application number")]
        public uint ApplicationNumber
        {
            get => InitialParametersClass.ApplicationNumber;
        }

        [JsonProperty("program id")]
        public string ProgramId
        {
            get => InitialParametersClass.ProgramIDTag;
        }

        [JsonProperty("program directory")]
        public string ProgramDirectory
        {
            get => InitialParametersClass.ProgramDirectory.ToString();
        }
    }
}