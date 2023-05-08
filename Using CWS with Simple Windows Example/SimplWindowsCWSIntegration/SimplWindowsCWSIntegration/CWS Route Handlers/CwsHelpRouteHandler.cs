using Crestron.SimplSharp.WebScripting;
using System.Net;

namespace SimplWindowsCWSIntegration
{
    internal class CwsHelpRouteHandler : CwsRouteHandler
    {

        public CwsHelpRouteHandler(RoomStatus status)
            : base(status) { }

        public override void ProcessRequest(HttpCwsContext context)
        {
            context.Response.WriteResponse(HttpStatusCode.OK, new { Help = "Help Response Goes Here" });
        }

        #region Setup
        internal override void RegisterRoutes(HttpCwsServer server)
        {

            // Create a route to get the value of {command} ie: control/power.
            server.Routes.Add(new HttpCwsRoute("help")
            {
                // Assign a unique name that can be used to identify an incoming request.
                Name = "HELP",
                // Rather than using the general handler a handler can be specified to handle this particular route directly.
                RouteHandler = this,
            });
        }
        #endregion
    }
}
