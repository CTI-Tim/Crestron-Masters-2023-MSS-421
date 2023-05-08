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
    [JsonObject(MemberSerialization.OptIn)]
    /// <summary>
    /// Route handler to process outgoing feedback to the 
    /// </summary>
    internal class CwsErrorLogRouteHandler : CwsRouteHandler

    {

        [JsonProperty("system status")]
        public string SystemStatus
        {
            get => status.Status;
        }

        [JsonProperty("error log")]
        public List<Error> Errors
        {
            get => status.LogEntries.ToList();
        }

        public CwsErrorLogRouteHandler(RoomStatus status)
            : base(status) { }

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
            server.Routes.Add(new HttpCwsRoute("log/{value}")
            {
                // Assign a unique name that can be used to identify an incoming request.
                Name = "ERRORLOG.SET",
                // Rather than using the general handler a handler can be specified to handle this particular route directly.
                RouteHandler = this
            });
        }

        public override void ProcessRequest(HttpCwsContext context)
        {
            try
            {
                // As an alternative to the previous approaches, we can also sort our requests using the route name assiged when the route was created.
                switch (context.Request.RouteData.Route.Name)
                {
                    case "ERRORLOG.GET":
                        context.Response.WriteResponse(HttpStatusCode.OK, this);
                        break;
                    case "ERRORLOG.SET":
                        {
                            if (context.Request.RouteData.Values.ContainsKey("value"))
                            {
                                string value = (context.Request.RouteData.Values["value"] as string).ToLower();
                                if (value == "clear")
                                {
                                    status.LogEntries.Clear();
                                    context.Response.WriteResponse(HttpStatusCode.OK, new { command = "clear", status = "ok" });
                                }
                                else
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
    }
}
