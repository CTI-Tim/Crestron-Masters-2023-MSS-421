using Crestron.SimplSharp;
using Crestron.SimplSharp.WebScripting;     // For CWS Support.
using System;
using System.Collections.Generic;
using System.Text;
using VC4Tools;

namespace SimplWindowsCWSIntegration
{
    public class CwsRoomAutomation
    {
        #region Readonly Properties
        /// <summary>
        /// Serializable data class storing the state of the room.
        /// </summary>
        private readonly RoomStatus status = new RoomStatus();
        /// <summary>
        /// Object to lock the server start / stop requests.
        /// </summary>
        private readonly object serverLock = new object();
        #endregion

        #region CWS Properties
        /// <summary>
        /// HttpCwsServer instance.
        /// </summary>
        private HttpCwsServer cwsServer;
        /// <summary>
        /// Route handler for dealing with commands such as power on / off / toggle.
        /// </summary>
        private readonly CwsCommandRouteHandler cwsCommandRouteHandler;
        /// <summary>
        /// Disable the warning that the following two clases aren't used so can be removed.
        /// Visual Studio doesn't recognise that they've been created and passed to the server.
        /// </summary>
#pragma warning disable IDE0052
        /// <summary>
        /// Route handler for dealing with error log & status reporting.
        /// </summary>
        private readonly CwsErrorLogRouteHandler cwsErrorLogRouteHandler;
        /// <summary>
        /// Route handler for dealing with help requests.
        /// </summary>
        private readonly CwsHelpRouteHandler cwsHelpRouteHandler;
        /// Restore the warning,
#pragma warning restore IDE0052
        /// <summary>
        /// General route handler which will handle all requests not dealt with elsewhere.
        /// </summary>
        private readonly GeneralCwsHandler generalCwsHandler;
        /// <summary>
        /// A collection of all our route handlers.
        /// </summary>
        private readonly List<CwsRouteHandler> routes;
        #endregion

        #region Event Handlers
        /// <summary>
        /// A power request has been received by the server.
        /// 
        /// Simpl+ can only subscribe to events in the base class. Rather than bubbling the event
        /// add / remove properties have been used to forward the event subscription to the specific handler.
        /// </summary>
        public event SimplPlusUshortEventHandler PowerRequest
        {
            add => cwsCommandRouteHandler.PowerRequest += value;
            remove => cwsCommandRouteHandler.PowerRequest -= value;
        }
        /// <summary>
        /// A system reset request has been received by the server.
        /// </summary>
        public event EventHandler SystemResetRequest
        {
            add => cwsCommandRouteHandler.SystemResetRequest += value;
            remove => cwsCommandRouteHandler.SystemResetRequest -= value;
        }
        /// <summary>
        /// A video mute request has been received by the server.
        /// </summary>
        public event SimplPlusUshortEventHandler VideoMuteRequest
        {
            add => cwsCommandRouteHandler.VideoMuteRequest += value;
            remove => cwsCommandRouteHandler.VideoMuteRequest -= value;
        }
        /// <summary>
        /// An audio mute request has been received by the server.
        /// </summary>
        public event SimplPlusUshortEventHandler AudioMuteRequest
        {
            add => cwsCommandRouteHandler.AudioMuteRequest += value;
            remove => cwsCommandRouteHandler.AudioMuteRequest -= value;
        }
        /// <summary>
        /// A volume request has been received by the server.
        /// </summary>
        public event SimplPlusUshortEventHandler VolumeRequest
        {
            add => cwsCommandRouteHandler.VolumeRequest += value;
            remove => cwsCommandRouteHandler.VolumeRequest -= value;
        }
        /// <summary>
        /// A volume level request has been received by the server.
        /// </summary>
        public event SimplPlusUshortEventHandler VolumeLevelRequest
        {
            add => cwsCommandRouteHandler.VolumeLevelRequest += value;
            remove => cwsCommandRouteHandler.VolumeLevelRequest -= value;
        }
        /// <summary>
        /// A lights request has been received by the server.
        /// </summary>
        public event SimplPlusUshortEventHandler LightsRequest
        {
            add => cwsCommandRouteHandler.LightsRequest += value;
            remove => cwsCommandRouteHandler.LightsRequest -= value;
        }
        /// <summary>
        /// A lighting level request has been received by the server.
        /// </summary>
        public event SimplPlusUshortEventHandler LightingLevelRequest
        {
            add => cwsCommandRouteHandler.LightingLevelRequest += value;
            remove => cwsCommandRouteHandler.LightingLevelRequest -= value;
        }
        /// <summary>
        /// A lighting scene request has been received by the server.
        /// </summary>
        public event SimplPlusUshortEventHandler LightingSceneRequest
        {
            add => cwsCommandRouteHandler.LightingSceneRequest += value;
            remove => cwsCommandRouteHandler.LightingSceneRequest -= value;
        }
        /// <summary>
        /// A shades request has been received by the server.
        /// </summary>
        public event SimplPlusUshortEventHandler ShadesRequest
        {
            add => cwsCommandRouteHandler.ShadesRequest += value;
            remove => cwsCommandRouteHandler.ShadesRequest -= value;
        }
        /// <summary>
        /// A source request has been received by the server.
        /// </summary>
        public event SimplPlusStringEventHandler SourceRequest
        {
            add => cwsCommandRouteHandler.SourceRequest += value;
            remove => cwsCommandRouteHandler.SourceRequest -= value;
        }
        /// <summary>
        /// A custom command has been received by the server.
        /// </summary>
        public event SimplPlusStringEventHandler CustomCommandRequest
        {
            add => cwsCommandRouteHandler.CustomCommandRequest += value;
            remove => cwsCommandRouteHandler.CustomCommandRequest -= value;
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Paremeterless constructor for Simpl+ compatability.
        /// </summary>
        public CwsRoomAutomation()
        {
            // Create a collection of the route handlers for this server.
            // These are created in the constructor so that Simpl+ can subscribe to them (indirectly).
            routes = new List<CwsRouteHandler>()
            {
                // Create an isntance of each of our handlers directly into the list.
                ( generalCwsHandler = new GeneralCwsHandler(status) ),
                ( cwsCommandRouteHandler = new CwsCommandRouteHandler(status) ),
                ( cwsErrorLogRouteHandler = new CwsErrorLogRouteHandler(status) ),
                ( cwsHelpRouteHandler = new CwsHelpRouteHandler(status) )
            };
        }
        #endregion

        #region Public Simpl+ Methods

        /// <summary>
        /// Start the server.
        /// </summary>
        /// <param name="path">The base path for the CWS server.</param>
        public void Start(string path)
        {
            lock (serverLock)
            {
                try
                {
                    // Check that server hasn't already been created.
                    if (cwsServer == null)
                    {
                        VC4Debugger.Debug("CWSRoomAutomation - Start() - Starting HttpCwsServer");

                        // If the server is running on an appliance, then append the program slot to the path.
                        var FullPath = AppendProgramSlot(path);

                        // Create an instance of HttpCwsServer at the specified path.
                        cwsServer = new HttpCwsServer(FullPath)
                        {
                            // Attach a general http request handler to the server.
                            // This handler can process all http requests, but for this example we are only processing requests with no route data.
                            // Unique handlers have been assigned to the individual routes independantly.
                            HttpRequestHandler = generalCwsHandler
                        };
                        // All requests are passed to the ReceivedRequest Event regardless of if they are handled directly.
                        // The subcription here is so that debug information can be sent.
                        cwsServer.ReceivedRequestEvent += HttpCwsServer_ReceivedRequestEvent;

                        // Register the routes with the server.
                        foreach (var cwsRoute in routes)
                        {
                            // Ask each route handler to register its routes with the server.
                            cwsRoute.RegisterRoutes(cwsServer);
                        }

                        // Register the server with the control system. All route assignments must have taken place before the instance is registered.
                        var result = cwsServer.Register();

                        VC4Debugger.Notice("CWSRoomAutomation - Start() - CWS Server Register Result: {0}", result ? "Success" : "Failure");

                        if (result)
                        {
                            VC4Debugger.Notice("CWSRoomAutomation - Start() - CWS Server Registered at: {0}", FullPath);
                        }
                    }
                    // else the server is already registered
                    else
                    {
                        // Send an error message to the debugger
                        VC4Debugger.Error("CWSRoomAutomation - Start() - Server Already Registered");
                    }
                }
                catch (Exception ex)
                {
                    VC4Debugger.Exception("CwsRoomAutomationExample - Start()", ex);
                    ErrorLog.Exception("CwsRoomAutomationExample - Start()", ex);
                }
            }
        }

        /// <summary>
        /// Stop the CWS Server.
        /// </summary>
        public void Stop()
        {
            lock (serverLock)
            {
                try
                {
                    // Check if a server exists).
                    if (cwsServer != null)
                    {
                        VC4Debugger.Debug("CWSRoomAutomation - Stop() - Stopping CWS HttpCwsServer");
                        // Unregister the server.
                        var result = cwsServer.Unregister();
                        // Dispose of the server.
                        cwsServer.Dispose();
                        cwsServer = null;

                        VC4Debugger.Notice("CWSRoomAutomation - Stop() - HttpCwsServer Unregister Result: {0}", result ? "Stopped" : "Failure");
                    }
                    // else the server is not running
                    else
                    {
                        // Send an error message to the debugger
                        VC4Debugger.Error("CWSRoomAutomation - Stop() - Server Not Running");
                    }
                }
                catch (Exception ex)
                {
                    VC4Debugger.Exception("CwsRoomAutomationExample - Stop()", ex);
                    ErrorLog.Exception("CwsRoomAutomationExample - Stop()", ex);
                }
            }
        }

        /// <summary>
        /// Store the power state for reporting to the cws client. 
        /// </summary>
        /// <param name="power">The power state to store.</param>
        public void SetPower(ushort power)
           => this.status.Power = power > 0;
        /// <summary>
        /// Store the video mute state for reporting to the cws client. 
        /// </summary>
        /// <param name="mute">The video mute state to store.</param>
        public void SetVideoMute(ushort mute)
            => this.status.VideoMute = mute > 0;
        /// <summary>
        /// Store the audio mute state for reporting to the cws client. 
        /// </summary>
        /// <param name="mute">The audio mute state to store.</param>
        public void SetAudioMute(ushort mute)
            => this.status.Volume.Mute = mute > 0;
        /// <summary>
        /// Store the volume level for reporting to the cws client. 
        /// </summary>
        /// <param name="level">The volume level to store.</param>
        public void SetVolume(ushort level)
            => this.status.Volume.Level = level;
        /// <summary>
        /// Store the lighting state for reporting to the cws client. 
        /// </summary>
        /// <param name="state">The lighting state to store.</param>
        public void SetLights(ushort state)
            => this.status.Lights.State = state > 0;
        /// <summary>
        /// Store the lighting level for reporting to the cws client. 
        /// </summary>
        /// <param name="level">The lighting level to store.</param>
        public void SetLightingLevel(ushort level)
            => this.status.Lights.Level = level;
        /// <summary>
        /// Store the lighting scene for reporting to the cws client. 
        /// </summary>
        /// <param name="scene">The lighting scene to store.</param>
        public void SetLightingScene(ushort scene)
            => this.status.Lights.Scene = scene;
        /// <summary>
        /// Store the shades state for reporting to the cws client. 
        /// </summary>
        /// <param name="state">The shades state to store.</param>
        public void SetShades(ushort state)
            => this.status.Shades = state > 0;
        /// <summary>
        /// Store the source for reporting to the cws client. 
        /// </summary>
        /// <param name="source">The source to store.</param>
        public void SetSource(string source)
            => this.status.Source = source;
        /// <summary>
        /// Store the system status for reporting to the cws client. 
        /// </summary>
        /// <param name="message">The system status to store.</param>
        public void SetStatus(string message)
            => this.status.Status = message;
        /// <summary>
        /// Store the error log entry for reporting to the cws client. 
        /// </summary>
        /// <param name="error">The error log entry to store.</param>
        public void LogError(string error)
            => this.status.LogEntries.Enqueue(new Error(error));
        #endregion

        #region Private Methods
        /// <summary>
        /// If the program is running on an appliance, then append the program slot to the path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>The path with the slot number if running on an applicance.</returns>
        private string AppendProgramSlot(string path)
        {
            // Create a string builder instance.
            StringBuilder build = new StringBuilder();
            // If we are running on an appliance then start the path with the slot.
            if (CrestronEnvironment.DevicePlatform == eDevicePlatform.Appliance)
                build.Append(string.Format("/slot{0}", InitialParametersClass.ApplicationNumber));
            // If the original path didn't start with a forward slash then add one to the string
            if (!path.StartsWith("/"))
                build.Append("/");
            // Add the path itself to the string
            build.Append(path);
            // If the string doesn't end with a forward slash then add one (not strictly necessary in this case).
            if (!path.EndsWith("/"))
                build.Append("/");
            // return the complete string.
            return build.ToString();
        }
        #endregion

        #region Event Handling 
        /// <summary>
        /// Catch-all Received Request Event from the CWS Server.
        /// We are not using this method in this example other than to print out information about each incoming request.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void HttpCwsServer_ReceivedRequestEvent(object sender, HttpCwsRequestEventArgs args)
        {
            try
            {
                VC4Debugger.Debug("Received Message from Server: {0} {1}", args.Context.Timestamp, args.Context.Request.Url);
                VC4Debugger.Debug("Path Info: {0}", args.Context.Request.PathInfo);
                VC4Debugger.Debug("Http Method: {0}", args.Context.Request.HttpMethod);
                VC4Debugger.Debug("Secure Connection: {0}", args.Context.Request.IsSecureConnection);
                VC4Debugger.Debug("Content Type: {0}", args.Context.Request.ContentType);

                // Another quick method of passing information into CWS is via QueryStrings.
                foreach (var key in args.Context.Request.QueryString.Keys)
                {
                    VC4Debugger.Debug("Query Key: {0} Value: {1}", key, args.Context.Request.QueryString[(string)key]);
                }

                VC4Debugger.Debug("User Host Address: {0}", args.Context.Request.UserHostAddress);
                VC4Debugger.Debug("User Hostname: {0}", args.Context.Request.UserHostName);
                VC4Debugger.Debug("User Agent: {0}", args.Context.Request.UserAgent);
                VC4Debugger.Debug("User Languages: {0}", string.Join(",", args.Context.Request.UserLanguages));
            }
            catch (Exception ex)
            {
                VC4Debugger.Exception("CwsRoomAutomation - HttpCwsServer_ReceivedRequestEvent(HttpCwsRequestEventArgs)", ex);
                ErrorLog.Exception("CwsRoomAutomation - HttpCwsServer_ReceivedRequestEvent(HttpCwsRequestEventArgs)", ex);
            }
        }
        #endregion
    }
}