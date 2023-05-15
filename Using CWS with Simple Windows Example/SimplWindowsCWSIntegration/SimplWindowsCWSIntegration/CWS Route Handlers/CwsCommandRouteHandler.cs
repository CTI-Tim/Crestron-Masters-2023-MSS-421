using Crestron.SimplSharp;
using Crestron.SimplSharp.WebScripting;
using SimplWindowsCWSIntegration.Converters;
using System;
using System.Collections.Generic;
using System.Net;
using VC4Tools;

namespace SimplWindowsCWSIntegration
{
    /// <summary>
    /// The route handler that deals with all commands such as power, lights & source.
    /// </summary>
    internal class CwsCommandRouteHandler : CwsRouteHandler
    {
        #region Private Readonly Properties
        /// <summary>
        /// A private dictionary mapping sub routes to each individual handler.
        /// </summary>
        private readonly IReadOnlyDictionary<string, Action<HttpCwsContext>> RouteDictionary;
        #endregion

        #region Event Handlers
        /// <summary>
        /// A power on / off / toggle request has been received by the CWS service.
        /// </summary>
        public event SimplPlusUshortEventHandler PowerRequest;
        /// <summary>
        /// A system reset request has been received by the CWS service.
        /// </summary>
        public event EventHandler SystemResetRequest;
        /// <summary>
        /// A video mute on / off / toggle request has been received by the CWS service.
        /// </summary>
        public event SimplPlusUshortEventHandler VideoMuteRequest;
        /// <summary>
        /// An audio mute on / off / toggle request has been received by the CWS service.
        /// </summary>
        public event SimplPlusUshortEventHandler AudioMuteRequest;
        /// <summary>
        /// A volume up / down request has been received by the CWS service.
        /// </summary>
        public event SimplPlusUshortEventHandler VolumeRequest;
        /// <summary>
        /// A volume level request has been received by the CWS service.
        /// </summary>
        public event SimplPlusUshortEventHandler VolumeLevelRequest;
        /// <summary>
        /// A lighting on / off / toggle request has been received by the CWS service.
        /// </summary>
        public event SimplPlusUshortEventHandler LightsRequest;
        /// <summary>
        /// A lighting scene request has been received by the CWS service.
        /// </summary>
        public event SimplPlusUshortEventHandler LightingSceneRequest;
        /// <summary>
        /// A lighting level request has been received by the CWS service.
        /// </summary>
        public event SimplPlusUshortEventHandler LightingLevelRequest;
        /// <summary>
        /// A shades open / close / stop request has been received by the CWS service.
        /// </summary>
        public event SimplPlusUshortEventHandler ShadesRequest;
        /// <summary>
        /// A source (string) request has been received by the CWS service.
        /// </summary>
        public event SimplPlusStringEventHandler SourceRequest;
        /// <summary>
        /// A custom command has been received by the CWS service.
        /// </summary>
        public event SimplPlusStringEventHandler CustomCommandRequest;
        #endregion

        #region Constructor
        public CwsCommandRouteHandler(RoomStatus status)
            : base(status)
        {
            // Create a dictionary mapping all the routes to their specific handler.
            RouteDictionary = new Dictionary<string, Action<HttpCwsContext>>()
            {
                {"power",HandlePowerRequest},
                {"reset",HandleSystemResetRequest},
                {"volume",HandleVolumeRequest},
                {"videomute",HandleVideoMuteRequest},
                {"audiomute",HandleAudioMuteRequest},
                {"lights",HandleLightingRequest},
                {"shades",HandleShadesRequest},
                {"source",HandleSourceRequest},
                {"custom",HandleCustomRequest},
            };
        }
        #endregion

        #region Public / Internal Methods
        /// <summary>
        /// Provides processing of HTTP requests by a HttpHandler that implements.
        /// the Crestron.SimplSharp.WebScripting.IHttpCwsHandler interface.
        /// </summary>
        /// <param name="context">The object encapsulating the HTTP request.</param>
        public override void ProcessRequest(HttpCwsContext context)
        {
            try
            {
                VC4Debugger.Debug("CwsCommandRouteHandler - Processing request for: {0}", context.Request.Url);
                // Retrieve the command name from the RouteData.Values.
                // Convert to lower case to avoid case sensitivity.
                string command = (context.Request.RouteData.Values["command"] as string).ToLower();
                // If the route is found in our dictionary, get the associated handler.
                if (RouteDictionary.TryGetValue(command, out var handler))
                {
                    // Run the handler and pass in the entire context, as the handler will finalise the process.
                    handler(context);
                }
                else
                {   // The route was not found so let the client know.
                    context.Response.WriteNotFound();
                    VC4Debugger.Error("CwsCommandRouteHandler - ProcessRequest() - Route Not Found");
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Exception("CwsCommandRouteHandler - ProcessRequest(HttpCwsContext)", ex);
            }
        }

        /// <summary>
        /// Registers the routes managed by this handler with the server.
        /// </summary>
        /// <param name="server">The HttpCwsServer to register the handler to.</param>
        internal override void RegisterRoutes(HttpCwsServer server)
        {

            // Create a route to get the value of {command} ie: control/power.
            server.Routes.Add(new HttpCwsRoute("control/{command}")
            {
                // Assign a unique name that can be used to identify an incoming request.
                Name = "CONTROL.COMMAND.GET",
                // Rather than using the general handler a handler can be specified to handle this particular route directly.
                RouteHandler = this,
            });

            // Create a route to set the {parameter} of {command} ie: control/power/on.
            server.Routes.Add(new HttpCwsRoute("control/{command}/{parameter}")
            {
                // Assign a unique name that can be used to identify an incoming request.
                Name = "CONTROL.COMMAND.SET",
                // Rather than using the general handler a handler can be specified to handle this particular route directly.
                RouteHandler = this,
            });

            // Create a route to set the {value} of {parameter} of {command} ie: control/lights/level/0.
            // An alternative approach would to be to use an HTTP PUT and pass the value as content.
            server.Routes.Add(new HttpCwsRoute("control/{command}/{parameter}/{value}")
            {
                // Assign a unique name that can be used to identify an incoming request.
                Name = "CONTROL.COMMAND.PARAMETER.SET",
                // Rather than using the general handler a handler can be specified to handle this particular route directly.
                RouteHandler = this,
            });
        }
        #endregion

        #region Command Handling / Processing
        /// <summary>
        /// Handles the individual power requests / responses.
        /// </summary>
        /// <param name="context">The context containing the request.</param>
        private void HandlePowerRequest(HttpCwsContext context)
        {
            try
            {
                // Try and get the value for the parameter.
                if (context.Request.RouteData.Values.TryGetValue("parameter", out object parameterObject))
                {
                    // Cast the parameter to a string.
                    string parameter = parameterObject as string;
                    // Check if the parameter is a valid value for our switch on / off / toggle enum.
                    if (Enum.TryParse<SwitchEventId>(parameter, true, out var result) && Enum.IsDefined(typeof(SwitchEventId), parameter))
                    {
                        // Invoke the power request event with a Simpl+ compatible ushort value to represent the switch enum requested.
                        PowerRequest?.Invoke(null, new SimplPlusUshortEventArgs((ushort)result));
                        // Echo the command back to the client with an ok status.
                        context.Response.WriteResponse(HttpStatusCode.OK, new { command = "power", parameter, status = "ok" });
                    }
                    else
                    {   // The parameter was not found or valid so let the client know that it wasn't recognised.
                        context.Response.WriteResponse(HttpStatusCode.BadRequest, new { command = "power", parameter, status = "not recognised" });
                    }
                }
                else
                {   // There was no parameter specified so this is a request. Return the current power status to the client.
                    context.Response.WriteResponse(HttpStatusCode.OK, new { power = status.Power }, new JsonOnOffBooleanConverter());
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Exception("CwsCommandRouteHandler - HandlePowerRequest(HttpCwsContext)", ex);
            }
        }

        /// <summary>
        /// Handles the system reset requests.
        /// </summary>
        /// <param name="context">The context containing the request.</param>
        private void HandleSystemResetRequest(HttpCwsContext context)
        {
            try
            {
                // This is a simple parameterless request, so invoke the SystemResetRequest event with an empty EventArgs.
                SystemResetRequest.Invoke(null, EventArgs.Empty);
                // Echo the command back to the client with an ok status.
                context.Response.WriteResponse(HttpStatusCode.OK, new { command = "reset", status = "ok" });
            }
            catch (Exception ex)
            {
                ErrorLog.Exception("CwsCommandRouteHandler - HandleSystemResetRequest(HttpCwsContext)", ex);
            }
        }

        /// <summary>
        /// Handles the volume requests / responses including up / down and discreet level.
        /// </summary>
        /// <param name="context">The context containing the request.</param>
        private void HandleVolumeRequest(HttpCwsContext context)
        {
            try
            {
                // Try and get the value for the parameter.
                if (context.Request.RouteData.Values.TryGetValue("parameter", out object parameterObject))
                {
                    // Cast the parameter to a string.
                    string parameter = parameterObject as string;
                    // Check if the parameter is a numerical value (discreet volume level).
                    if (ushort.TryParse(parameter, out ushort value))
                    {
                        // Invoke the volume level request event and pass in the volume level.
                        VolumeLevelRequest?.Invoke(null, new SimplPlusUshortEventArgs(value));
                    }
                    // Check if the parameter is a valid value for our volume enum.
                    else if (Enum.TryParse<VolumeEventId>(parameter, true, out var result) && Enum.IsDefined(typeof(VolumeEventId), parameter))
                    {
                        // Invoke the volume request event with a Simpl+ compatible ushort value to represent the volume enum requested.
                        VolumeRequest?.Invoke(null, new SimplPlusUshortEventArgs((ushort)result));
                    }
                    else
                    {   // The parameter was not found or valid so let the client know that it wasn't recognised.
                        context.Response.WriteResponse(HttpStatusCode.BadRequest, new { command = "volume", parameter, status = "not recognised" });
                        // Quit as the line below sends the ok response for all items above.
                        return;
                    }
                    // Echo the command back to the client with an ok status.
                    context.Response.WriteResponse(HttpStatusCode.OK, new { command = "volume", parameter, status = "ok" });
                }
                else
                {   // There was no parameter specified so this is a request. Return the current volume status to the client.
                    context.Response.WriteResponse(HttpStatusCode.OK, new { volume = status.Volume });
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Exception("CwsCommandRouteHandler - HandleVolumeRequest(HttpCwsContext)", ex);
            }
        }

        /// <summary>
        /// Handles the video mute requests / responses.
        /// </summary>
        /// <param name="context">The context containing the request.</param>
        private void HandleVideoMuteRequest(HttpCwsContext context)
        {
            try
            {
                // Try and get the value for the parameter.
                if (context.Request.RouteData.Values.TryGetValue("parameter", out object parameterObject))
                {
                    // Cast the parameter to a string.
                    string parameter = parameterObject as string;
                    // Check if the parameter is a valid value for our switch on / off / toggle enum.
                    if (Enum.TryParse<SwitchEventId>(parameter, true, out var result) && Enum.IsDefined(typeof(SwitchEventId), parameter))
                    {
                        // Invoke the video mute request event with a Simpl+ compatible ushort value to represent the switch enum requested.
                        VideoMuteRequest?.Invoke(null, new SimplPlusUshortEventArgs((ushort)result));
                        // Echo the command back to the client with an ok status.
                        context.Response.WriteResponse(HttpStatusCode.OK, new { command = "videomute", parameter, status = "ok" });
                    }
                    else
                    {   // The parameter was not found or valid so let the client know that it wasn't recognised.
                        context.Response.WriteResponse(HttpStatusCode.BadRequest, new { command = "videomute", parameter, status = "not recognised" });
                    }
                }
                else
                {   // There was no parameter specified so this is a request. Return the current video mute status to the client.
                    context.Response.WriteResponse(HttpStatusCode.OK, new { videomute = status.VideoMute }, new JsonMuteBooleanConverter());
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Exception("CwsCommandRouteHandler - HandleVideoMuteRequest(HttpCwsContext)", ex);
            }
        }

        /// <summary>
        /// Handles the audio mute requests / responses.
        /// </summary>
        /// <param name="context">The context containing the request.</param>
        private void HandleAudioMuteRequest(HttpCwsContext context)
        {
            try
            {
                // Try and get the value for the parameter.
                if (context.Request.RouteData.Values.TryGetValue("parameter", out object parameterObject))
                {
                    // Cast the parameter to a string.
                    string parameter = parameterObject as string;
                    // Check if the parameter is a valid value for our switch on / off / toggle enum.
                    if (Enum.TryParse<SwitchEventId>(parameter, true, out var result) && Enum.IsDefined(typeof(SwitchEventId), parameter))
                    {
                        // Invoke the audio mute request event with a Simpl+ compatible ushort value to represent the switch enum requested.
                        AudioMuteRequest?.Invoke(null, new SimplPlusUshortEventArgs((ushort)result));
                        // Echo the command back to the client with an ok status.
                        context.Response.WriteResponse(HttpStatusCode.OK, new { command = "audiomute", parameter, status = "ok" });
                    }
                    else
                    {   // The parameter was not found or valid so let the client know that it wasn't recognised.
                        context.Response.WriteResponse(HttpStatusCode.BadRequest, new { command = "audiomute", parameter, status = "not recognised" });
                    }
                }
                else
                {   // There was no parameter specified so this is a request. Return the current audio mute status to the client.
                    context.Response.WriteResponse(HttpStatusCode.OK, new { audiomute = status.Volume.Mute }, new JsonMuteBooleanConverter());
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Exception("CwsCommandRouteHandler - HandleAudioMuteRequest(HttpCwsContext)", ex);
            }
        }

        /// <summary>
        /// Handles the lighting requests / responses.
        /// </summary>
        /// <param name="context">The context containing the request.</param>
        private void HandleLightingRequest(HttpCwsContext context)
        {
            // TODO: Refactor this, as it is convoluted.
            try
            {   
                // For code readability, take a quick reference of Values for use further down.
                var values = context.Request.RouteData.Values;
                // Try and get the value for the parameter.
                if (values.TryGetValue("parameter", out object parameterObject))
                {
                    // Cast the parameter to a string.
                    string parameter = parameterObject as string;
                    // Check if the parameter is a valid value for our lighting enum.
                    if (Enum.TryParse<LightingEventId>(parameter, true, out var result) && Enum.IsDefined(typeof(LightingEventId), parameter))
                    {
                        // Invoke the lights request event with a Simpl+ compatible ushort value to represent the lighting enum requested.
                        LightsRequest?.Invoke(null, new SimplPlusUshortEventArgs((ushort)result));
                        // Echo the command back to the client with an ok status.
                        context.Response.WriteResponse(HttpStatusCode.OK, new { command = "lights", parameter, status = "ok" });
                    } // Try and get the value of the 'value' and if successful, try and parse that value to a ushort.
                    else if (values.TryGetValue("value", out object value) && ushort.TryParse(value as string, out ushort scene))
                    {
                        // lookup the remaining parameters
                        switch (parameter.ToLower())
                        {
                            case "scene":
                                // Invoke the lighting scene level request event and pass in the lighting scene.
                                LightingSceneRequest?.Invoke(null, new SimplPlusUshortEventArgs(scene));
                                break;
                            case "level":
                                // Invoke the lighting level request event and pass in the lighting level.
                                LightingLevelRequest?.Invoke(null, new SimplPlusUshortEventArgs(scene));
                                break;
                            default:
                                // The parameter was not found or valid so let the client know that it wasn't recognised.
                                context.Response.WriteResponse(HttpStatusCode.BadRequest, new { command = "lights", parameter, value, status = "not recognised" });
                                return;
                        }
                        // Echo the command back to the client with an ok status.
                        context.Response.WriteResponse(HttpStatusCode.OK, new { command = "lights", parameter, value, status = "ok" });
                    }
                    else
                    {   // The parameter was not found or valid so let the client know that it wasn't recognised.
                        context.Response.WriteResponse(HttpStatusCode.BadRequest, new { command = "lights", parameter, status = "not recognised" });
                    }
                }
                else
                {   // There was no parameter specified so this is a request. Return the current lights status to the client.
                    context.Response.WriteResponse(HttpStatusCode.OK, new { lights = status.Lights });
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Exception("CwsCommandRouteHandler - HandleLightingRequest(HttpCwsContext)", ex);
            }
        }

        /// <summary>
        /// Handles the shade requests / responses.
        /// </summary>
        /// <param name="context">The context containing the request.</param>
        private void HandleShadesRequest(HttpCwsContext context)
        {
            try
            {
                // Try and get the value for the parameter.
                if (context.Request.RouteData.Values.TryGetValue("parameter", out object parameterObject))
                {
                    // Cast the parameter to a string.
                    string parameter = parameterObject as string;
                    // Check if the parameter is a valid value for our shades enum.
                    if (Enum.TryParse<ShadesEventId>(parameter, true, out var result) && Enum.IsDefined(typeof(ShadesEventId), parameter))
                    {
                        // Invoke the shades request event with a Simpl+ compatible ushort value to represent the shade enum requested.
                        ShadesRequest?.Invoke(null, new SimplPlusUshortEventArgs((ushort)result));
                        // Echo the command back to the client with an ok status.
                        context.Response.WriteResponse(HttpStatusCode.OK, new { command = "shades", parameter, status = "ok" });
                    }
                    else
                    {   // The parameter was not found or valid so let the client know that it wasn't recognised.
                        context.Response.WriteResponse(HttpStatusCode.BadRequest, new { command = "shades", parameter, status = "not recognised" });
                    }
                }
                else
                {   // There was no parameter specified so this is a request. Return the current shades status to the client.
                    context.Response.WriteResponse(HttpStatusCode.OK, new { shades = status.Shades }, new JsonShadesBooleanConverter());
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Exception("CwsCommandRouteHandler - HandleShadesRequest(HttpCwsContext)", ex);
            }
        }

        /// <summary>
        /// Handles the source requests / responses.
        /// </summary>
        /// <param name="context">The context containing the request.</param>
        private void HandleSourceRequest(HttpCwsContext context)
        {
            try
            {
                // Try and get the value for the parameter.
                if (context.Request.RouteData.Values.TryGetValue("parameter", out object parameterObject))
                {
                    // Cast the parameter to a string.
                    string parameter = parameterObject as string;
                    // Invoke the source request event with the parameter (source) passed as the argument.
                    SourceRequest?.Invoke(null, new SimplPlusStringEventArgs(parameter));
                    // Echo the command back to the client with an ok status.
                    context.Response.WriteResponse(HttpStatusCode.OK, new { source = parameter });
                }
                else
                {   // There was no parameter specified so this is a request. Return the current source status to the client.
                    context.Response.WriteResponse(HttpStatusCode.OK, new { source = status.Source });
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Exception("CwsCommandRouteHandler - HandleSourceRequest(HttpCwsContext)", ex);
            }
        }

        /// <summary>
        /// Handles custom requests.
        /// </summary>
        /// <param name="context">The context containing the request.</param>
        private void HandleCustomRequest(HttpCwsContext context)
        {
            try
            {
                // Try and get the value for the parameter.
                if (context.Request.RouteData.Values.TryGetValue("parameter", out object parameterObject))
                {
                    // Cast the parameter to a string.
                    string parameter = parameterObject as string;
                    // Invoke the custom command request event with the parameter (custom command) passed as the argument.
                    CustomCommandRequest?.Invoke(null, new SimplPlusStringEventArgs(parameter));
                    // Echo the command back to the client with an ok status.
                    context.Response.WriteResponse(HttpStatusCode.OK, new { command = "custom", parameter, status = "ok" });
                }
                else
                {
                    context.Response.WriteResponse(HttpStatusCode.BadRequest, new { command = "custom", status = "missing parameter" });
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Exception("CwsCommandRouteHandler - HandleCustomRequest(HttpCwsContext)", ex);
            }
        }
        #endregion
    }
}