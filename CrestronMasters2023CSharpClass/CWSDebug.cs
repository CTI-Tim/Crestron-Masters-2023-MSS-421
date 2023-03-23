
using Crestron.SimplSharp;
using Crestron.SimplSharp.Net;
using Crestron.SimplSharp.WebScripting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CrestronMasters2023CSharpClass
{
    /*
     * Woah wait!  A static class?   reddit and the INTERNET says all static classes are code smell and bad to use!    
     * Static classes have their place when used correctly. For example, a debug class that will be used everywhere in the program
     * is a great example of a good use of a static class. Technically the Config class should be static as well if you needed to access
     * the configuration information in all your other classes in your program.  Keep this in mind as you design your programs.
     */

    static class CWSDebug
    {
        private static bool _initialized = false;
        private static bool _debug = false;
        private static List<string> _messages = new List<string>(50);
        private static Dictionary<string, Delegate> Commands = new Dictionary<string, Delegate>();

        private static HttpCwsRoute myRoute;
        private static HttpCwsServer myServer;

        static CWSDebug()
        {
            /*
             * A static constructor is used to initialize any static data,
             * or to perform a particular action that needs to be performed only once.
             * It is called automatically before the first instance is created or any static members are referenced.
             * A static constructor will be called at most once.
             * A static constructor is called automatically to initialize the class BEFORE the first instance is created.
             * You cannot pass parameters to a static class constructor.
             *
             * Because we do not instantiate a static class we do not want to use this like we do with a public class
             * Instead Static Classes should use a static initialization method.
             */
        }

        /// <summary>
        /// You must call the Init method before trying to use any of the CWSDebug class. the class will not function
        /// at all until it is called.
        /// </summary>
        /// <param name="ServerPath"></param>
        public static void Init(string path, string route)
        {
            myServer = new HttpCwsServer("/" + path);

            myRoute = new HttpCwsRoute(route + "/{DATA}");
            myRoute.RouteHandler = new DebugTerminalHandler();

            myServer.AddRoute(myRoute);                                 // Finally add the route to the server
            if (myServer.Register())
            {
                CWSDebug.Msg($"Sucessfully Registered server at cws/{path}/{route}");

                _initialized = true;
            }
            else
            {
                CWSDebug.Msg($"Failed to register {path}/{route} with CWS");
                _initialized = false;
            }
            CWSDebug.Msg(" Registered Server and route, now adding commands ");
            AddCommands();
        }

        public static void Msg(string s)
        {
            CrestronConsole.PrintLine(s);


            string payload = DateTime.Now.ToString("yyyy/MM/dd-HH:mm:ss") + " - " + s;

            /*
             *  WE want to keep a rolling list that does not consume all of available memory.  we set the capacity
             *  when we defined the list, so now we check and see if we reached that capacity, and if so, we delete
             *  the oldest one which happens to be at index 0.   
             */

            if (_messages.Count == _messages.Capacity)
            {
                _messages.RemoveAt(0);  //This is  a lot easier in C# than what we did in 301 in S+!
            }

            _messages.Add(payload);

        }

        private static void MyServer_ReceivedRequestEvent(object sender, HttpCwsRequestEventArgs args)
        {
            CrestronConsole.PrintLine("CWS Request:{0} from {1}",
                args.Context.Request.HttpMethod,
                            args.Context.Request.UserHostAddress);

            // Example to look for specific info in the Get URL sent
            // This does not care about any routes and allows you to look for anything that was sent
            // to the root of the Server path specified.    This lets you do simple things quick and dirty
            // without having to create another Route and Route processing class
            CrestronConsole.PrintLine("RAW = {0}", args.Context.Request.RawUrl);


            // Get for requests for information
            if (args.Context.Request.HttpMethod == "GET")
            {
                /*
                 *  Below we are going to parse the raw URL sent to talk to this server, this is going to
                 *  look for parameters and extract the data if we find what we are looking for.
                 *  Note: this only works here. you cannot look for parameters in the separate
                 *  route handlers as sending a parameter will make them never match and contain
                 *  the desired data.  If you want to support parameters this is the way.
                 *  in a web browser go to http://ipaddressofprocessor/cws/api/?command=test
                 *  if you add a second parameter it would be http://ipaddressofprocessor/cws/api/?command=test&other=test
                 */
                var RawUrl = args.Context.Request.RawUrl;  // we want the raw URL as this has the info we are after

                // ParseQueryString can't parse a whole URL, we just want the parameters so split on the ? and grab the last
                var parameters = HttpUtility.ParseQueryString(RawUrl.Split('?').Last());
                var command = parameters.Get("command");
                if (command != null)
                {
                    CrestronConsole.PrintLine("Command found  and it contains {0}", command);

                    //Sends a response back to the web browser, we can use the args.Context.Response Object to send a full response
                    //Be aware to do this timely the connection will time out if you take too long to send your response
                    args.Context.Response.StatusCode = 200;
                    args.Context.Response.ContentType = "text/plain";
                    args.Context.Response.Write("Got Command " + command, true);
                }


                /*
                 * Or you can do it manually as well
                // Now we just treat it as a string and look for and extract the data we are after
                if (RawUrl.Contains("?command="))
                {
                    var a = RawUrl.LastIndexOf("?command=");
                    var b = RawUrl.Length;
                    CrestronConsole.PrintLine("Command that was sent  = {0}",RawUrl.Substring(a+9, b-a-9));
                }
                */
            }
            // Posts for updating and changing information
            else if (args.Context.Request.HttpMethod == "POST")
            {
                var RawUrl = args.Context.Request.RawUrl;  // we want the raw URL as this has the info we are after

                // ParseQueryString can't parse a whole URL, we just want the parameters so split on the ? and grab the last
                var parameters = HttpUtility.ParseQueryString(RawUrl.Split('?').Last());
                var command = parameters.Get("command");
                if (command != null)
                    CrestronConsole.PrintLine("Command found  and it contains {0}", command);
            }
        }

        private class DebugTerminalHandler : IHttpCwsHandler
        {
            public void ProcessRequest(HttpCwsContext context)
            {
                //_context = context;
                var requestMethod = context.Request.HttpMethod;
                string terminalCommand = "";


                // Let's get any data that was sent back from the javascript from JqueryTerminal and clean it up.
                using (var myReader = new Crestron.SimplSharp.CrestronIO.StreamReader(context.Request.InputStream))
                {
                    // we are going to get command=XXXX  we need to get rid of  command=  split is really easy way of doing that
                    var splits = myReader.ReadToEnd().Trim().ToLower().Split('=');
                    terminalCommand = splits[1];
                }

                switch (requestMethod)
                {
                    case "GET":
                        {
                            if (context.Request.RouteData.Values.ContainsKey("DATA"))
                            {

                            }
                            else
                            {
                                context.Response.Write($"ERROR: a GET request is unrecognized", true);
                            }

                            GenerateResponseHeader(context);
                            break;
                        }
                    case "POST":
                        {


                            string[] words = terminalCommand.Split('+');


                            if (Commands.ContainsKey(words[0]))
                            {
                                // Check if the command had a parameter after it
                                if (words.Length < 2)
                                {

                                    Commands[words[0]].DynamicInvoke("", context);
                                }
                                else
                                {
                                    Commands[words[0]].DynamicInvoke(words[1], context);
                                }
                            }
                            else
                            {
                                context.Response.Write(
                                    "ERROR: command not found.  You can use the help command for a list of what is available.",
                                    false);
                            }
                            break;
                        }
                }
                context.Response.Write("\r\n", true);  // We need to send the last response with a final true. do not forget this
                GenerateResponseHeader(context);
            }

            /*
             * Method to generate the mostly static header information that never really changes in this instance.
             * if you need to change the header based on what you are sending back then you would need to set the content
             * type dynamically in your code.
             */
            private void GenerateResponseHeader(HttpCwsContext c)
            {
                c.Response.StatusCode = 200;
                c.Response.ContentType = "text/plain";
            }


        }
        // ##################################################################################################
        // ######                   Add commands for Simulated Terminal Handler  below here
        // ##################################################################################################
        private static void AddCommands()
        {
            //We load up our dictionary with the command names and create a delegate to each one of our methods below.
            Commands.Add("help", new Func<string, HttpCwsContext, bool>(TermHelp));
            Commands.Add("%3f", new Func<string, HttpCwsContext, bool>(TermHelp)); // %3f is what is sent from http for a ?
            Commands.Add("last", new Func<string, HttpCwsContext, bool>(MsgList));
            Commands.Add("debug", new Func<string, HttpCwsContext, bool>(DbugCommand));
            Commands.Add("info", new Func<string, HttpCwsContext, bool>(ProgInfo));
            CWSDebug.Msg($"Added {Commands.Count} Commands to the dictionary");
        }

        // Return type needs to be bool just in case we need to return that it was successful
        private static bool TermHelp(string command, HttpCwsContext context)
        {
            context.Response.Write("\r\n\r\n-- Masters 2023 CWS Debug Console Help --\r\n", false);
            context.Response.Write("-----------------------------------------\r\n\r\n", false);
            context.Response.Write(" last [1-50] = show the last debug messages (Limited to the last 50)\r\n", false);
            context.Response.Write(" debug [on/off] = set debug to on or off, anything else reports current setting\r\n", false);
            context.Response.Write(" info = Program Instance information\r\n", false);
            context.Response.Write(" help = list this help\r\n", false);

            return true;
        }
        // List what messages are currently stored.
        private static bool MsgList(string command, HttpCwsContext context)
        {
            var num = 0;
            int.TryParse(command, out num);
            if (num > _messages.Count || num < 0)  // sanity checking as you can not trust users

                num = 0;

            CWSDebug.Msg($" last shows we requested {num}");
            if (num < 1)
            {
                foreach (string s in _messages)
                {
                    context.Response.Write($"{s}\r\n", false);
                }

                context.Response.Write($"\r\n Returned {_messages.Count} debug messages", false);
            }
            else
            {
                for (int i = _messages.Count - num; i < _messages.Count; i++)
                {
                    context.Response.Write($"{_messages[i]}\r\n", false);
                }
            }

            return true;
        }

        private static bool DbugCommand(string command, HttpCwsContext context)
        {

            switch (command)
            {
                case "on":
                    _debug = true;
                    context.Response.Write($"Debug is now ON", false);
                    break;


                case "off":
                    _debug = false;
                    context.Response.Write($"Debug is now OFF", false);
                    break;

                default:
                    if (_debug)
                        context.Response.Write($"Debug is ON", false);
                    else
                        context.Response.Write($"Debug is OFF", false);
                    break;
            }
            return true;
        }

        private static bool ProgInfo(string command, HttpCwsContext context)
        {
            context.Response.Write($"\r\nProgram ID Tag = {InitialParametersClass.ProgramIDTag} \r\n", false);
            context.Response.Write($"Program RoomID = {InitialParametersClass.RoomId} \r\n", false);
            context.Response.Write($"Application Number = {InitialParametersClass.ApplicationNumber} \r\n", false);
            context.Response.Write($"Room Name = {InitialParametersClass.RoomName} \r\n", false);
            context.Response.Write($"Platform = {InitialParametersClass.ControllerPromptName} \r\n", false);
            context.Response.Write($"Firmware Ver = {InitialParametersClass.FirmwareVersion} \r\n", false);
            context.Response.Write($"Current IP address = {CrestronEthernetHelper.GetEthernetParameter(CrestronEthernetHelper.ETHERNET_PARAMETER_TO_GET.GET_CURRENT_IP_ADDRESS, 0)} \r\n", false);


            return true;
        }

    }
}
