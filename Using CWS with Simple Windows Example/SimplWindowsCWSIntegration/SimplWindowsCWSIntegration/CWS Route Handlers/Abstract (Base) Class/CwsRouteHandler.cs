using Crestron.SimplSharp.WebScripting;

namespace SimplWindowsCWSIntegration
{
    internal abstract class CwsRouteHandler : IHttpCwsHandler
    {
        #region Readonly Properties
        /// <summary>
        /// Store a reference to the status model so we can report back to the client.
        /// </summary>
        protected readonly RoomStatus status;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for CwsRouteHandler.
        /// </summary>
        /// <param name="status">The room status model that all subscribers use.</param>
        protected CwsRouteHandler(RoomStatus status)
        {
            this.status = status;
        }
        #endregion

        #region Public / Internal Methods
        /// <summary>
        /// Provides processing of HTTP requests by a HttpHandler that implements.
        /// the Crestron.SimplSharp.WebScripting.IHttpCwsHandler interface.
        /// </summary>
        /// <param name="context">The object encapsulating the HTTP request.</param>
        public abstract void ProcessRequest(HttpCwsContext context);

        /// <summary>
        /// Registers the routes this handler will accept.
        /// All routes must be registered with the CWS server
        /// before the server itself is registered with the control system.
        /// </summary>
        /// <param name="server">The server instance to register the routes to.</param>
        internal abstract void RegisterRoutes(HttpCwsServer server);
        #endregion
    }
}
