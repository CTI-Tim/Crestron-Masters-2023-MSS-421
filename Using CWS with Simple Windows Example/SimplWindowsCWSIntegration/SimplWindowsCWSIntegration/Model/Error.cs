using Newtonsoft.Json;
using SimplWindowsCWSIntegration.Converters;
using System;

namespace SimplWindowsCWSIntegration
{
    internal class Error
    {
        /// <summary>
        /// The timestamp of the error message in the format yyyy-MM-dd HH:mm:ss.
        /// </summary>
        [JsonProperty("timestamp")]
        [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd HH:mm:ss")]
        public DateTime Timestamp;

        /// <summary>
        /// The error message logged.
        /// </summary>
        [JsonProperty("message")]
        public string Message;

        public Error(string message)
        {
            this.Timestamp = DateTime.Now;
            this.Message = message;
        }
    }
}
