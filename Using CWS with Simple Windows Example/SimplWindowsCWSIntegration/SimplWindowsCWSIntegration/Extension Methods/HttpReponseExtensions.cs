using Crestron.SimplSharp.WebScripting;
using Newtonsoft.Json;
using System.Net;

namespace SimplWindowsCWSIntegration
{
    internal static class HttpResponseExtensions
    {

        /// <summary>
        /// Set successful response parameters
        /// </summary>
        /// <param name="response"></param>
        /// <param name="content"></param>
        public static void WriteResponse(this HttpCwsResponse response, HttpStatusCode statusCode, object content)
        {
            WriteResponse(response, statusCode, JsonConvert.SerializeObject(content, Formatting.Indented));
        }

        /// <summary>
        /// Set successful response parameters
        /// </summary>
        /// <param name="response"></param>
        /// <param name="content"></param>
        public static void WriteResponse(this HttpCwsResponse response, HttpStatusCode statusCode, object content, params JsonConverter[] converters)
        {
            WriteResponse(response, statusCode, JsonConvert.SerializeObject(content, Formatting.Indented, converters));
        }

        /// <summary>
        /// Set successful response parameters
        /// </summary>
        /// <param name="response"></param>
        /// <param name="content"></param>
        public static void WriteResponse(this HttpCwsResponse response, HttpStatusCode statusCode, string content)
        {
            response.StatusCode = (int)statusCode;
            response.ContentType = "application/json";
            response.Write(content, true);
        }
    }
}
