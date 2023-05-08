using Crestron.SimplSharp;
using Crestron.SimplSharp.WebScripting;
using Independentsoft.Exchange;
using SimplWindowsCWSIntegration.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace SimplWindowsCWSIntegration
{
    internal class CwsCommandRouteHandler : CwsRouteHandler
    {

        private readonly IReadOnlyDictionary<string, Action<HttpCwsContext>> CommandDictionary;

        #region Event Handlers
        public event SimplPlusUshortEventHandler PowerRequest;

        public event EventHandler SystemResetRequest;

        public event SimplPlusUshortEventHandler VideoMuteRequest;

        public event SimplPlusUshortEventHandler AudioMuteRequest;

        public event SimplPlusUshortEventHandler VolumeRequest;
        public event SimplPlusUshortEventHandler VolumeLevelRequest;

        public event SimplPlusUshortEventHandler LightsRequest;
        public event SimplPlusUshortEventHandler LightingSceneRequest;
        public event SimplPlusUshortEventHandler LightingLevelRequest;

        public event SimplPlusUshortEventHandler ShadesRequest;

        public event SimplPlusStringEventHandler SourceRequest;

        public event SimplPlusStringEventHandler CustomCommandRequest;
        #endregion

        public CwsCommandRouteHandler(RoomStatus status)
            : base(status)
        {
            CommandDictionary = new Dictionary<string, Action<HttpCwsContext>>()
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

        public override void ProcessRequest(HttpCwsContext context)
        {
            try
            {
                CrestronConsole.PrintLine("CwsCommandRouteHandler - IHttpCwsHandler.ProcessRequest(HttpCwsContext) - {0}", context.Request.Url);

                string command = (context.Request.RouteData.Values["command"] as string).ToLower();

                // Get the http method
                string HttpMethod = context.Request.HttpMethod;

                if (CommandDictionary.ContainsKey(command))
                    CommandDictionary[command](context);

            }
            catch (Exception ex)
            {
                ErrorLog.Exception("CwsCommandRouteHandler - IHttpCwsHandler.ProcessRequest(HttpCwsContext)", ex);
            }
        }

        #region Setup
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
        private void HandlePowerRequest(HttpCwsContext context)
        {
            try
            {
                if (context.Request.RouteData.Values.TryGetValue("parameter", out object parameterObject))
                {
                    string parameter = parameterObject as string;
                    if (Enum.TryParse<SwitchEventId>(parameter, true, out var result) && Enum.IsDefined(typeof(SwitchEventId), parameter))
                    {
                        PowerRequest?.Invoke(null, new SimplPlusUshortEventArgs((ushort)result));
                        context.Response.WriteResponse(HttpStatusCode.OK, new { command = "power", parameter, status = "ok" });
                    }
                    else
                    {
                        context.Response.WriteResponse(HttpStatusCode.BadRequest, new { command = "power", parameter, status = "not recognised" });
                    }
                }
                else
                {
                    context.Response.WriteResponse(HttpStatusCode.OK, new { power = status.Power }, new JsonOnOffBooleanConverter());
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Exception("CwsCommandRouteHandler - HandlePowerRequest(HttpCwsContext)", ex);
            }
        }

        private void HandleSystemResetRequest(HttpCwsContext context)
        {
            try
            {
                SystemResetRequest.Invoke(null, EventArgs.Empty);
                context.Response.WriteResponse(HttpStatusCode.OK, new { command = "reset", status = "ok" });
            }
            catch (Exception ex)
            {
                ErrorLog.Exception("CwsCommandRouteHandler - HandleSystemResetRequest(HttpCwsContext)", ex);
            }
        }

        private void HandleVolumeRequest(HttpCwsContext context)
        {
            try
            {
                if (context.Request.RouteData.Values.TryGetValue("parameter", out object parameterObject))
                {
                    string parameter = parameterObject as string;

                    if (ushort.TryParse(parameter, out ushort value))
                    {
                        VolumeLevelRequest?.Invoke(null, new SimplPlusUshortEventArgs(value));
                    }
                    else if (Enum.TryParse<VolumeEventId>(parameter, true, out var result) && Enum.IsDefined(typeof(VolumeEventId), parameter))
                    {
                        VolumeRequest?.Invoke(null, new SimplPlusUshortEventArgs((ushort)result));
                    }
                    else
                    {
                        context.Response.WriteResponse(HttpStatusCode.BadRequest, new { command = "volume", parameter, status = "not recognised" });
                        return;
                    }
                    context.Response.WriteResponse(HttpStatusCode.OK, new { command = "volume", parameter, status = "ok" });
                }
                else
                    context.Response.WriteResponse(HttpStatusCode.OK, new { volume = status.Volume });
            }
            catch (Exception ex)
            {
                ErrorLog.Exception("CwsCommandRouteHandler - HandleVolumeRequest(HttpCwsContext)", ex);
            }
        }

        private void HandleVideoMuteRequest(HttpCwsContext context)
        {
            try
            {
                if (context.Request.RouteData.Values.TryGetValue("parameter", out object parameterObject))
                {
                    string parameter = parameterObject as string;

                    if (Enum.TryParse<SwitchEventId>(parameter, true, out var result) && Enum.IsDefined(typeof(SwitchEventId), parameter))
                    {
                        VideoMuteRequest?.Invoke(null, new SimplPlusUshortEventArgs((ushort)result));
                        context.Response.WriteResponse(HttpStatusCode.OK, new { command = "videomute", parameter, status = "ok" });
                    }
                    else
                    {
                        context.Response.WriteResponse(HttpStatusCode.BadRequest, new { command = "videomute", parameter, status = "not recognised" });
                    }
                }
                else
                {
                    context.Response.WriteResponse(HttpStatusCode.OK, new { videomute = status.VideoMute }, new JsonMuteBooleanConverter());
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Exception("CwsCommandRouteHandler - HandleVideoMuteRequest(HttpCwsContext)", ex);
            }
        }

        private void HandleAudioMuteRequest(HttpCwsContext context)
        {
            try
            {
                if (context.Request.RouteData.Values.TryGetValue("parameter", out object parameterObject))
                {
                    string parameter = parameterObject as string;

                    if (Enum.TryParse<SwitchEventId>(parameter, true, out var result) && Enum.IsDefined(typeof(SwitchEventId), parameter))
                    {
                        AudioMuteRequest?.Invoke(null, new SimplPlusUshortEventArgs((ushort)result));
                        context.Response.WriteResponse(HttpStatusCode.OK, new { command = "audiomute", parameter, status = "ok" });
                    }
                    else
                    {
                        context.Response.WriteResponse(HttpStatusCode.BadRequest, new { command = "audiomute", parameter, status = "not recognised" });
                    }
                }
                else
                {
                    context.Response.WriteResponse(HttpStatusCode.OK, new { audiomute = status.Volume.Mute }, new JsonMuteBooleanConverter());
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Exception("CwsCommandRouteHandler - HandleAudioMuteRequest(HttpCwsContext)", ex);
            }
        }

        private void HandleLightingRequest(HttpCwsContext context)
        {
            try
            {
                var values = context.Request.RouteData.Values;
                if (values.TryGetValue("parameter", out object parameterObject))
                {
                    string parameter = parameterObject as string;

                    if (Enum.TryParse<LightingEventId>(parameter, true, out var result) && Enum.IsDefined(typeof(LightingEventId), parameter))
                    {
                        LightsRequest?.Invoke(null, new SimplPlusUshortEventArgs((ushort)result));
                        context.Response.WriteResponse(HttpStatusCode.OK, new { command = "lights", parameter, status = "ok" });
                    }
                    else if (values.TryGetValue("value", out object value) && ushort.TryParse(value as string, out ushort scene))
                    {
                        switch (parameter.ToLower())
                        {
                            case "scene":
                                LightingSceneRequest?.Invoke(null, new SimplPlusUshortEventArgs(scene));
                                break;
                            case "level":
                                LightingLevelRequest?.Invoke(null, new SimplPlusUshortEventArgs(scene));
                                break;
                            default:
                                context.Response.WriteResponse(HttpStatusCode.BadRequest, new { command = "lights", parameter, value, status = "not recognised" });
                                return;
                        }

                        context.Response.WriteResponse(HttpStatusCode.OK, new { command = "lights", parameter, value, status = "ok" });
                    }
                    else
                    {
                        context.Response.WriteResponse(HttpStatusCode.BadRequest, new { command = "lights", parameter, status = "not recognised" });
                    }
                }
                else
                {
                    context.Response.WriteResponse(HttpStatusCode.OK, new { lights = status.Lights });
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Exception("CwsCommandRouteHandler - HandleLightingRequest(HttpCwsContext)", ex);
            }
        }

        private void HandleShadesRequest(HttpCwsContext context)
        {
            try
            {
                if (context.Request.RouteData.Values.TryGetValue("parameter", out object parameterObject))
                {
                    string parameter = parameterObject as string;

                    if (Enum.TryParse<ShadesEventId>(parameter, true, out var result) && Enum.IsDefined(typeof(ShadesEventId), parameter))
                    {
                        ShadesRequest?.Invoke(null, new SimplPlusUshortEventArgs((ushort)result));
                        context.Response.WriteResponse(HttpStatusCode.OK, new { command = "shades", parameter, status = "ok" });
                    }
                    else
                    {
                        context.Response.WriteResponse(HttpStatusCode.BadRequest, new { command = "shades", parameter, status = "not recognised" });
                    }
                }
                else
                {
                    context.Response.WriteResponse(HttpStatusCode.OK, new { shades = status.Shades }, new JsonShadesBooleanConverter());
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Exception("CwsCommandRouteHandler - HandleShadesRequest(HttpCwsContext)", ex);
            }
        }

        private void HandleSourceRequest(HttpCwsContext context)
        {
            try
            {
                if (context.Request.RouteData.Values.TryGetValue("parameter", out object parameterObject))
                {
                    string parameter = parameterObject as string;
                    SourceRequest?.Invoke(null, new SimplPlusStringEventArgs(parameter));
                    context.Response.WriteResponse(HttpStatusCode.OK, new { source = parameter });
                }
                else
                {
                    context.Response.WriteResponse(HttpStatusCode.OK, new { source = status.Source });
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Exception("CwsCommandRouteHandler - HandleSourceRequest(HttpCwsContext)", ex);
            }
        }
        private void HandleCustomRequest(HttpCwsContext context)
        {
            try
            {
                if (context.Request.RouteData.Values.TryGetValue("parameter", out object parameterObject))
                {
                    string parameter = parameterObject as string;
                    CustomCommandRequest?.Invoke(null, new SimplPlusStringEventArgs(parameter));
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