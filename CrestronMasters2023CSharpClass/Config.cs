using Crestron.SimplSharp;
using Crestron.SimplSharp.Net;
using Crestron.SimplSharp.WebScripting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace CrestronMasters2023CSharpClass

{
    // Yes this is the Config Class from Masters 2022 MSS-431. Instead of using XML this one uses JSON it still saves and reads the config file.
    public class Config
    {
        //Globals
        private string _filename = "";
        public static Configuration Setting;  //Static?  YES!  This makes sure we have only ONE as a static member meaning it can exist only once.

        private HttpCwsRoute myRoute;
        private HttpCwsServer myServer;

        /// <summary>
        /// Create a copy of the Config class and define the filename used for this config
        /// </summary>
        /// <param name="Filename"> Just the filename without any extension or any path information.</param>
        public Config(string Filename)
        {
            Setting = new Configuration();                              // Create an instance of our Configuration class that will
                                                                        // be available as ClassName.Setting when we instantiate the
                                                                        // config class

            _filename = CheckFilename(Filename);                        // Is the filename valid and add in a path if one is not
            CWSDebug.Msg($"ProgID = {InitialParametersClass.ProgramIDTag}");
            CWSDebug.Msg($"RoomID = {InitialParametersClass.RoomId}");
            CWSDebug.Msg($"RoomName = {InitialParametersClass.RoomName}");
            CWSDebug.Msg($"ControllerPrompt = {InitialParametersClass.ControllerPromptName}");
            CWSDebug.Msg($"ProgramDirectory = {InitialParametersClass.ProgramDirectory}");
            Cws("app", "settings"); // Create the CWS server for the settings
            // /cws/app/settings
        }
        /// <summary>
        /// Saves the configuration data to storage.
        /// </summary>
        public void Save()
        {
            try
            {
                CWSDebug.Msg("Saving to " + _filename);
                string payload = JsonConvert.SerializeObject(Setting);  //Using Newtonsoft lets turn the class members into a Json String
                TextWriter filestream = new StreamWriter(_filename);
                filestream.Write(payload);
                filestream.Close();
                CWSDebug.Msg("File Save Closed");
            }
            catch (Exception ex)
            {
                // Print out the Exception a little cleaner
                string a = ex.ToString();
                string[] b = a.Split('\x0d');
                foreach (string b2 in b)
                    CWSDebug.Msg(b2);
            }
        }
        /// <summary>
        /// Loads the Configuration file from storage.
        /// </summary>
        public void Load()
        {
            XmlSerializer seralizer = new XmlSerializer(typeof(Configuration));      // Create a serializer that has the structure of our class

            if (File.Exists(_filename))                                              // Check if the file even exists before we try and open it
            {
                CWSDebug.Msg("File does exist loading");

                using (FileStream stream = File.OpenRead(_filename))                 // Open our file
                {
                    TextReader reader = new StreamReader(_filename);
                    Setting = JsonConvert.DeserializeObject<Configuration>(reader.ReadToEnd());
                }
            }
            else
            {
                CWSDebug.Msg("File Does not Exist");
                // If a configuration file does not exist then this is a good place to create a default set of configuration settings
            }
        }

        private void Cws(string path, string route)
        {
            myServer = new HttpCwsServer("/" + path);
            myRoute = new HttpCwsRoute(route + "/{REQUEST}");       // Create the route with the Data Token inserted
            myRoute.RouteHandler = new ConfigRequestHandler();          // Give route a copy of the handler class
            myServer.AddRoute(myRoute);                                 // Finally add the route to the server
            if (myServer.Register())
            {
                CWSDebug.Msg($"Sucessfully Registered server at cws/{path}/{route}");
                myServer.ReceivedRequestEvent += MyServer_ReceivedRequestEvent;
            }
            else
            {
                CWSDebug.Msg($"Failed to register {path}/{route} with CWS");
            }
        }

        // Below is bonus information if you need to have a HTTP style API interface
        private void MyServer_ReceivedRequestEvent(object sender, HttpCwsRequestEventArgs args)
        {
            //TODO: Add special URL handling

            // Example to look for specific info in the Get URL sent
            // This does not care about any routes and allows you to look for anything that was sent
            // to the root of the Server path specified.    This lets you do simple things quick and dirty
            // without having to create another Route and Route processing class.  THIS IS NOT RESTFUL

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
                    CWSDebug.Msg(string.Format("Command found  and it contains {0}", command));

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
                    CWSDebug.Msg(string.Format("Command found  and it contains {0}", command));
            }


        }



        /// <summary>
        /// Checks the Filename to be valid for appliance or server use and corrects it
        /// </summary>
        /// <param name="Filename"></param>
        /// <returns>string with corrected path</returns>
        private string CheckFilename(string Filename)
        {
            /*
             *  Things are different on VC-4 versus a 4 series processor.  Specifically the file locations.  On the server we need to get the
             *  whole path.    On a processor we need to append the programID tag to emulate behavior that Simpl and simpl+ uses
             */

            if (Filename.Length < 5)  // an invalid filename was specified a.txt is the least acceptable here
                Filename = InitialParametersClass.ProgramIDTag + ".txt";  // Default to the program tag.txt
            // NOTE: We use a .txt extension here so you can easily view the file.  using a .json extension or even .cfg is acceptable as well


            var RootDir = Crestron.SimplSharp.CrestronIO.Directory.GetApplicationRootDirectory();  // get the root directory
            if (CrestronEnvironment.DevicePlatform == eDevicePlatform.Server)  // are we on VC-4?
            {
                CWSDebug.Msg(" On VC-4 application is at |" + RootDir + "|");
                return RootDir + "/user/" + Filename;
            }
            else
            {
                // If we are on a processor to emulate S+ behavior we are going to append the ProgramNameTag to the path to create that folder.

                var Path = "/user/" + InitialParametersClass.ProgramIDTag + "/";  // Lowercase!  Case is important now and User will create a new TLD

                if (!Directory.Exists(Path))            // If the directory does not exist, make it.  With great power comes great responsibility.
                    Directory.CreateDirectory(Path);

                return Path + Filename;
            }
        }

    }

    public class ConfigRequestHandler : IHttpCwsHandler
    {
        private HttpCwsContext _context;
        public void ProcessRequest(HttpCwsContext context)
        {
            _context = context;
            var requestMethod = _context.Request.HttpMethod;
            var requestContents = "";
            // Let's get any data that was sent back from the javascript on the web page and clean it up.
            using (var myreader = new Crestron.SimplSharp.CrestronIO.StreamReader(_context.Request.InputStream))
            {
                requestContents = myreader.ReadToEnd().Trim();
            }

            switch (requestMethod)
            {
                /*
                 *  in the REST definition a GET is for asking for information.   we want to GET The current settings
                 *  Here we only support sending the whole thing or only the endpoints as an example of how to deliver
                 *  a subset of information in case you wanted to only access a smaller amount of information or specific
                 *  information from the server
                 */
                case "GET":
                    {
                        // Request from the webpage to GET the settings, so lets send out the json that has the settings.
                        if (_context.Request.RouteData.Values.ContainsKey("REQUEST"))
                        {
                            if (_context.Request.RouteData.Values["REQUEST"].ToString().ToLower() == "endpoints")
                            {
                                CWSDebug.Msg("EndPoint Request");
                                string response = JsonConvert.SerializeObject(Config.Setting.Endpoints);
                                GenerateResponseHeader();
                                _context.Response.Write(response, true);
                            }
                            else if (_context.Request.RouteData.Values["REQUEST"].ToString().ToLower() == "all")
                            {
                                CWSDebug.Msg("All Request");
                                string response = JsonConvert.SerializeObject(Config.Setting);
                                GenerateResponseHeader();
                                _context.Response.Write(response, true);
                            }
                            else
                            {
                                _context.Response.Write($"ERROR: {_context.Request.RouteData.Values["REQUEST"].ToString()} is unrecognized", true);
                            }

                        }
                        return;
                    }
                case "POST":
                    {
                        /* NOTE:  we MUST use the Crestron StreamReader here instead of System.IO.StreamReader as what comes out of CWS is
                        *        defined in the CrestronIO namespace.  Be aware of this.
                        *
                        * WARNING!  There is no sanity checking on the json that is sent in from the web page and javascript
                        *           A complete solution will do some data analysis and cleaning before processing.
                        *           Best Practice is to NOT trust data incoming from the user.
                        *
                        * This is where we get a POST from a calling webpage or device or service.  POST defines in REST that we are sending
                        * data to the server or in this case the CWS server  our program created.  All the code below will take what was sent
                        * in the json payload and write the changes to our configuration object.
                        *
                        * NOTE:  we are technically not 100% correct here for a REST response as we are not sending a response payload of JSON back.
                        *        that is an exercise left up to the student to add into the code later.
                        *
                        */

                        // Look to see if we were sent data in the REQUEST. /cws/app/settings/endpoints for example
                        if (_context.Request.RouteData.Values.ContainsKey("REQUEST"))
                        {
                            if (_context.Request.RouteData.Values["REQUEST"].ToString().ToLower() == "endpoints")
                            {
                                using (var myreader =
                                       new Crestron.SimplSharp.CrestronIO.StreamReader(_context.Request.InputStream))
                                {
                                    // We only want to update the endpoint list so we have to reference it properly as a List<NVX>
                                    // This will overwrite the whole list, not append to it.

                                    Config.Setting.Endpoints = JsonConvert.DeserializeObject<List<NVX>>(requestContents);
                                }
                                _context.Response.Write("OK", true);
                            }
                            else if (_context.Request.RouteData.Values["REQUEST"].ToString().ToLower() == "all")
                            {
                                using (var myreader =
                                       new Crestron.SimplSharp.CrestronIO.StreamReader(_context.Request.InputStream))
                                {
                                    Config.Setting = JsonConvert.DeserializeObject<Configuration>(requestContents);
                                }
                                _context.Response.Write("Endpoint OK", true);
                            }


                            
                            else if (_context.Request.RouteData.Values["REQUEST"].ToString().ToLower() == "add")
                            {
                                // This means we need to PARSE the JSON being sent and extract the  IP address and endpoint name.
                                // Hint: we already have a class below that defines the NVX endpoint,  you can make a local instance 
                                // here to make your job easier extracting the information
                                using (var myreader =
                                       new Crestron.SimplSharp.CrestronIO.StreamReader(_context.Request.InputStream))
                                {
                                    NVX newEndpoint = JsonConvert.DeserializeObject<NVX>(requestContents);
                                    CWSDebug.Msg($"New endpoint {newEndpoint.Name} at {newEndpoint.Address}");
                                    Config.Setting.Endpoints.Add(new NVX { Address = newEndpoint.Address, Name = newEndpoint.Name });
                                }

                            }
                            
                            else if (_context.Request.RouteData.Values["REQUEST"].ToString().ToLower() == "del")
                            {
                                // The task at hand should be the same decode the name at least we need to find in the list and
                                // write the code to delete it

                                using (var myreader =
                                       new Crestron.SimplSharp.CrestronIO.StreamReader(_context.Request.InputStream))
                                {
                                    NVX newEndpoint = JsonConvert.DeserializeObject<NVX>(requestContents);

                                    var i = 0;
                                    foreach (var item in Config.Setting.Endpoints)
                                    {
                                        if (item.Name == newEndpoint.Name)
                                            break;
                                        i++;
                                    }

                                    if (i >= 0 && i <= Config.Setting.Endpoints.Count - 1)
                                    {
                                        CWSDebug.Msg($"Index found at {i} Deleting {newEndpoint.Name}");
                                        Config.Setting.Endpoints.RemoveAt(i);
                                    }
                                    else
                                    {
                                        CWSDebug.Msg($"Index NOT found.");
                                    }
                                }

                            }
                            else
                            {
                                _context.Response.Write($"ERROR: {_context.Request.RouteData.Values["REQUEST"].ToString()} is unrecognized", true);
                            }
                        }

                        GenerateResponseHeader();
                        return;
                    }
            }
        }

        /*
         * Method to generate the mostly static header information that never really changes in this instance.
         * if you need to change the header based on what you are sending back then you would need to set the content
         * type dynamically in your code.
         */
        private void GenerateResponseHeader()
        {
            _context.Response.StatusCode = 200;
            _context.Response.ContentType = "text/plain";
        }
    }

    public class Configuration
    {
        public string MastersClass = "";
        public string IPAddress = "";
        public ushort Port = 0;
        public string UiPassword = "";

        public List<NVX> Endpoints;

        public Configuration()
        {
            Endpoints = new List<NVX>(); // need to instantiate the list when the class is instantiated
        }

    }

    public class NVX
    {
        public string Address = "";
        public string Name = "";
    }

    /*
     * Here are some important details about Json serialization in C# that you should be aware of. First of all, every class
     * that we want to serialize should define the default (parameterless) constructor. 
     * In our case, we have not defined any constructors on the NVX class. Therefore, the parameterless constructor is included by default.
     * Also, only the public members of a class will be serialized; private members will be omitted from the XML document.
     * Any properties must have a public getter method. If they also have a setter method, this must be public too.
     * If your Setter is not Public then it cannot be updated when loaded.
     * Be careful using any properties with code to calculate other properties, the calculated ones will be saved and attempt to
     * be loaded when the file is loaded.
     * 
     * You CAN serialize a class that contains more classes as seen above.  For example you want a list of objects that contain information
     * about your infinetEX dimmers.  
     */
}

