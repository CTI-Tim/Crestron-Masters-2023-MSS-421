using Crestron.SimplSharp.WebScripting;
using System.Net;
using System;
using System.Runtime.Remoting.Contexts;

namespace SimplWindowsCWSIntegration
{
    /// <summary>
    /// General route handler to pick up all unhandled routes.
    /// </summary>
    internal class GeneralCwsHandler : CwsRouteHandler
    {
        #region Constructor
        public GeneralCwsHandler(RoomStatus status)
            : base(status) { }
        #endregion

        #region Public / Internal Methods
        /// <summary>
        /// Not used as this is the general handler for all unhandled routes. 
        /// </summary>
        /// <param name="server"></param>
        internal override void RegisterRoutes(HttpCwsServer server)
        {
        }

        /// <summary>
        /// Provides processing of HTTP requests by a HttpHandler that implements.
        /// the Crestron.SimplSharp.WebScripting.IHttpCwsHandler interface.
        /// </summary>
        /// <param name="context">The object encapsulating the HTTP request.</param>
        public override void ProcessRequest(HttpCwsContext context)
        {
            // We can read the paths here and respond accordingly.
            // If this is the base path, (with or without the forward slash /) then retun the system status.
            if (String.IsNullOrEmpty(context.Request.PathInfo) || context.Request.PathInfo == "/")
            {   
                // Return the full system status to the user.
                context.Response.WriteResponse(HttpStatusCode.OK, status);
            }
            else
            {   // This is not the base path and no other handler has picked up the request.
                // Write an Error 404 response as the path was not found in our system.
                context.Response.WriteNotFound();
            }
        }
        #endregion
    }
}
