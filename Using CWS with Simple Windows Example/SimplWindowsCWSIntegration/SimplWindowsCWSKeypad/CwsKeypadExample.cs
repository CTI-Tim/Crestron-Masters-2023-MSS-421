using Crestron.SimplSharp;
using Crestron.SimplSharp.WebScripting;
using System;
using System.Net;

namespace SimplWindowsCWSKeypad
{
    /// <summary>
    /// A minimal implemenation of the Crestron Web Services class to pass button press events to Simpl Windows.
    /// The module is a stripped-back example with the intention of showing the very basics of CWS.
    /// The module registers a single route path/button/{value} with the CWS server instance and publishes events to Simpl+
    /// containing the index of the button 'pressed' / 'triggered' from the CWS API.
    /// </summary>
    public class CwsKeypadExample
    {
        #region Properties
        /// <summary>
        /// The HttpCwsServer instance.
        /// </summary>
        private HttpCwsServer server;
        /// <summary>
        /// HttpCwsHandler for our specific route.
        /// </summary>
        private readonly HttpCwsHandler handler = new HttpCwsHandler();
        #endregion

        #region Event Handlers
        /// <summary>
        /// EventHandler to publish button press events to the Simpl+ wrapper.
        /// 
        /// Simpl+ can only subscribe to events in the base class. Rather than bubbling the event
        /// add / remove properties have been used to forward the event subscription to the child class.
        /// </summary>
        public event SimplPlusButtonEventHandler ButtonPressed
        {
            add => handler.ButtonPressed += value;
            remove => handler.ButtonPressed -= value;
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Empty constructor for Simpl+ compatability.
        /// </summary>
        public CwsKeypadExample()
        {
        }

        /// <summary>
        /// Initialises the CwsKeypadExample class and registers the CWS server to the supplied path.
        /// </summary>
        /// <param name="path">The base path for the CWS server.</param>
        public void Initialise(string path)
        {
            try
            {
                // Null check the server to ensure that this class is not accidentally called twice.
                if (server != null) { return; }

                // Create a new HttpCwsServer instance at the provided path.
                server = new HttpCwsServer(path);

                // Create a new HttpCwsRoute at the fixed subdirectoy /button/ with the variable {value}.
                var route = new HttpCwsRoute("button/{value}")
                {
                    // Attach our route handler to the route.
                    RouteHandler = handler,
                };

                // Add the route to the server's route collection.
                server.Routes.Add(route);

                // Register the server.
                // All routes must be set up before the server is registered, else they will be ignored.
                server.Register();
            }
            catch(Exception ex)
            {
                // Log any exceptions that occured when handling the request.
                ErrorLog.Exception("CwsKeypadExample - Initialise(HttpCwsContext)", ex);
            }
        }
        #endregion

        /// <summary>
        /// HttpCwsHandler implementation provided as a private nested class. 
        /// AFAIK. Due to limitations of Simpl+, the CwsKeypadExample class cannot implement IHttpCwsHandler directly.
        /// </summary>
        private class HttpCwsHandler : IHttpCwsHandler
        {
            #region Event Handlers
            /// <summary>
            /// A button press request has been received by the CWS server.
            /// </summary>
            public event SimplPlusButtonEventHandler ButtonPressed;
            #endregion

            #region IHttpCwsHandler Implementation 
            /// <summary>
            /// Provides processing of HTTP requests by a HttpHandler that implements.
            /// the Crestron.SimplSharp.WebScripting.IHttpCwsHandler interface.
            /// </summary>
            /// <param name="context">The object encapsulating the HTTP request.</param>
            void IHttpCwsHandler.ProcessRequest(HttpCwsContext context)
            {
                try
                {
                    // Set the response content type to text.
                    context.Response.ContentType = "text/plain";

                    // Try and parse the value object to a ushort (0-65535).
                    if (ushort.TryParse(context.Request.RouteData.Values["value"] as string, out ushort buttonIndex))
                    {
                        // If the TryParse succeeded then publish the ButtonPressed event containing the button index that was requested. 
                        ButtonPressed?.Invoke(null, new SimplPlusButtonEventArgs(buttonIndex));
                        // Set the statuscode to ok.
                        context.Response.StatusCode = (int)HttpStatusCode.OK;
                        // Write a simple string to let the sender know that their request has been received.
                        context.Response.Write(string.Format("Button {0} Pressed", buttonIndex), true);
                    }
                    else // The result of "value" was not a valid ushort value
                    {
                        // Set the statuscode to badrequest.
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        // Write a simple string to let the sender know that their request wasn't valid.
                        context.Response.Write(string.Format("Error - {0} is not a valid number within the range 0-65535", buttonIndex), true);
                    }
                }
                catch (Exception ex)
                {
                    // Log any exceptions that occured when handling the request.
                    // As this is only a minimal example, there is limited error checking within the handler.
                    ErrorLog.Exception("HttpCwsHandler - ProcessRequest(HttpCwsContext)", ex);
                }
            }
            #endregion
        }
    }
}