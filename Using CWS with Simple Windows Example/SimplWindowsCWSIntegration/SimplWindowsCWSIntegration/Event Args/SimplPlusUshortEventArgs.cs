using System;

namespace SimplWindowsCWSIntegration
{
    public delegate void SimplPlusUshortEventHandler(object sender, SimplPlusUshortEventArgs e);
    public class SimplPlusUshortEventArgs : EventArgs
    {
        public ushort Value;

        public SimplPlusUshortEventArgs()
        {

        }

        public SimplPlusUshortEventArgs(ushort value)
        {
            this.Value = value;
        }
    }
}