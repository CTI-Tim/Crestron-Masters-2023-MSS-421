using Crestron.SimplSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SimplWindowsCWSIntegration
{
    /// <summary>
    /// Serialisable class encapsulating basic information about the system.
    /// </summary>
    internal class SystemInfo
    {
        [JsonProperty("platform")]
        [JsonConverter(typeof(StringEnumConverter))]
        public eDevicePlatform Platform
        {
            get => CrestronEnvironment.DevicePlatform;
        }

        [JsonProperty("firmware")]
        public string Firmware
        {
            get => CrestronEnvironment.OSVersion.Firmware;
        }

        [JsonProperty("serial number")]
        public string SerialNumber
        {
            get => CrestronEnvironment.SystemInfo.SerialNumber;
        }

        [JsonProperty("ram free")]
        public string RamFree
        {
            get => string.Format("{0} / {1}", CrestronEnvironment.SystemInfo.RamFree, CrestronEnvironment.SystemInfo.TotalRamSize);
        }
    }
}
