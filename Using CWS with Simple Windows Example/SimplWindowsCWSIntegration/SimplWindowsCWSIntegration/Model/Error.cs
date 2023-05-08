using Newtonsoft.Json;
using SimplWindowsCWSIntegration.Converters;
using System;

namespace SimplWindowsCWSIntegration
{
    internal class Error
    {
        [JsonProperty("timestamp")]
        [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd HH:mm:ss")]
        public DateTime dateTime;

        [JsonProperty("message")]
        public string message;

        public Error(string message)
        {
            dateTime = DateTime.Now;
            this.message = message;
        }
    }
}
