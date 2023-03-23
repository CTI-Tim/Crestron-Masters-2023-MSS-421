using Crestron.SimplSharp;                          	// For Basic SIMPL# Classes
using Crestron.SimplSharpPro;                       	// For Basic SIMPL#Pro classes
using Crestron.SimplSharpPro.CrestronThread;        	// For Threading
using Crestron.SimplSharpPro.UI;
using System;

namespace CrestronMasters2023CSharpClass
{
    public class ControlSystem : CrestronControlSystem
    {
        // Create our globals
        private Config myConfig;
        private Password myPassword;
        private XpanelForSmartGraphics myTp;

        public ControlSystem()
            : base()
        {
            Thread.MaxNumberOfUserThreads = 20;
        }


        public override void InitializeSystem()
        {
            try
            {
                CWSDebug.Msg("Start of ControlSystem.InitializeSystem()"); // load a message before we even initialize the class!

                myConfig = new Config("Masters2023Settings");
                Config.Setting.MastersClass = "Masters2023";
                Config.Setting.UiPassword = "12345";
                Config.Setting.Port = 63200;
                Config.Setting.IPAddress = "127.0.0.1";
                Config.Setting.Endpoints.Add(new NVX { Address = "123.456.789.000", Name = "Main Ballroom" });
                Config.Setting.Endpoints.Add(new NVX { Address = "127.0.0.1", Name = "Grotto" });
                Config.Setting.Endpoints.Add(new NVX { Address = "10.10.10.10", Name = "Garage HotTub" });

                myPassword = new Password();

                myTp = new XpanelForSmartGraphics(0x03, this);

                CWSDebug.Init("debug", "term");


            }
            catch (Exception e)
            {
                ErrorLog.Error("Error in InitializeSystem: {0}", e.Message);
            }
        }

    }
}