using Crestron.SimplSharp.WebScripting;
using System.Net;

namespace SimplWindowsCWSIntegration
{
    /// <summary>
    /// Handles the help request route.
    /// </summary>
    internal class CwsHelpRouteHandler : CwsRouteHandler
    {
        #region Constructor
        public CwsHelpRouteHandler(RoomStatus status)
            : base(status) { }
        #endregion

        #region Public / Internal Methods
        /// <summary>
        /// Provides processing of HTTP requests by a HttpHandler that implements.
        /// the Crestron.SimplSharp.WebScripting.IHttpCwsHandler interface.
        /// </summary>
        /// <param name="context">The object encapsulating the HTTP request.</param>
        public override void ProcessRequest(HttpCwsContext context)
        {
            // TODO: Create a custom help response for the user.
            context.Response.WriteResponse(HttpStatusCode.OK, new { Help = "Help can be found by pressing F1 on the Simpl Windows user modules." });
        }

        /// <summary>
        /// Registers the routes managed by this handler with the server.
        /// </summary>
        /// <param name="server">The HttpCwsServer to register the handler to.</param>
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
