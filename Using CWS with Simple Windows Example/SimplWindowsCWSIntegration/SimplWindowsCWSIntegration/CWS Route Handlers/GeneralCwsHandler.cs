using Crestron.SimplSharp.WebScripting;
using System.Net;


namespace SimplWindowsCWSIntegration
{
    internal class GeneralCwsHandler : CwsRouteHandler
    {
        private const string basePath = "/";

        public GeneralCwsHandler(RoomStatus status)
            : base(status) { }


        /// <summary>
        /// Not used as this is just to demonstrate 
        /// </summary>
        /// <param name="server"></param>
        internal override void RegisterRoutes(HttpCwsServer server)
        {
        }

        public override void ProcessRequest(HttpCwsContext context)
        {
            // We can read the paths here and respond accordingly.
            if (context.Request.PathInfo == basePath)
            {
                context.Response.WriteResponse(HttpStatusCode.OK, status);
            }
        }
    }
}
