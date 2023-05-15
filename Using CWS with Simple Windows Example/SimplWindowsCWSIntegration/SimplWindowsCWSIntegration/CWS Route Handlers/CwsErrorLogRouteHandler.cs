using Crestron.SimplSharp;
using Crestron.SimplSharp.WebScripting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using VC4Tools;

namespace SimplWindowsCWSIntegration
{
    // Only serialise members that have opted in.
    [JsonObject(MemberSerialization.OptIn)]
    /// <summary>
    /// Route handler to process log / status feedback to the client.
    /// </summary>
    internal class CwsErrorLogRouteHandler : CwsRouteHandler
    {
        #region Serialisable Properties
        /// <summary>
        /// The system status message.
        /// </summary>
        [JsonProperty("system status")]
        public string SystemStatus
        {
            // Fetch the status from the model.
            get => status.Status;
        }
        /// <summary>
        /// The current X entries of the non-persistent error log.
        /// </summary>
        [JsonProperty("error log")]
        public List<Error> Errors
        {
            // Take a copy of the error log list from the model.
            get => status.LogEntries.ToList();
        }
        #endregion

        #region Constructor
        public CwsErrorLogRouteHandler(RoomStatus status)
            : base(status) { }
        #endregion

        #region Public / Internal Methods
        /// <summary>
        /// Registers the routes managed by this handler with the server.
        /// </summary>
        /// <param name="server">The HttpCwsServer to register the handler to.</param>
        internal override void RegisterRoutes(HttpCwsServer server)
        {
            // Create a direct route to request the values of the error log and add the server
            server.Routes.Add(new HttpCwsRoute("log")
            {
                // Assign a unique name that can be used to identify an incoming request.
                Name = "ERRORLOG.GET",
                // Rather than using the general handler a handler can be specified to handle this particular route directly.
                RouteHandler = this
            });

            // Create a route to log/{value} for future expansion.
            server.Routes.Add(new HttpCwsRoute("log/{parameter}")
            {
                // Assign a unique name that can be used to identify an incoming request.
                Name = "ERRORLOG.SET",
                // Rather than using the general handler a handler can be specified to handle this particular route directly.
                RouteHandler = this
            });
        }

        /// <summary>
        /// Provides processing of HTTP requests by a HttpHandler that implements.
        /// the Crestron.SimplSharp.WebScripting.IHttpCwsHandler interface.
        /// </summary>
        /// <param name="context">The object encapsulating the HTTP request.</param>
        public override void ProcessRequest(HttpCwsContext context)
        {
            try
            {
                // As an alternative to the previous approaches, we can also sort our requests using the route name assiged when the route was created.
                switch (context.Request.RouteData.Route.Name)
                {
                    // A get request has been received.
                    case "ERRORLOG.GET":
                        // Serialise the public members of this class (status & log) and return the data to the client.
                        context.Response.WriteResponse(HttpStatusCode.OK, this);
                        break;
                    // A set request has been received.
                    case "ERRORLOG.SET":
                        {
                            // Try and get the parameter from the Values. This shouldn't fail in this case as it is determined by the route.
                            if (context.Request.RouteData.Values.TryGetValue("parameter", out var value))
                            {
                                // If the value (cast as a string) equals clear.
                                if ((value as string).Equals("clear",StringComparison.CurrentCultureIgnoreCase))
                                {
                                    // Clear the status log.
                                    status.LogEntries.Clear();
                                    // Echo the command back to the client with an ok status.
                                    context.Response.WriteResponse(HttpStatusCode.OK, new { command = "clear", status = "ok" });
                                }
                                else
                                    // The parameter was not found or valid so let the client know that it wasn't recognised.
                                    context.Response.WriteResponse(HttpStatusCode.BadRequest, new { command = "error", value, status = "not recognised" });
                            }
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                VC4Debugger.Exception("CwsErrorLogRouteHandler - IHttpCwsHandler.ProcessRequest(HttpCwsContext)", ex);
                ErrorLog.Exception("CwsErrorLogRouteHandler - IHttpCwsHandler.ProcessRequest(HttpCwsContext)", ex);
            }
        }
        #endregion
    }
}
