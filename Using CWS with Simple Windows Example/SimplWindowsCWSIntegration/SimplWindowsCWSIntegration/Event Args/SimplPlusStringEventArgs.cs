using System;

namespace SimplWindowsCWSIntegration
{
    public delegate void SimplPlusStringEventHandler(object sender, SimplPlusStringEventArgs e);

    public class SimplPlusStringEventArgs : EventArgs
    {
        public string Value;

        public SimplPlusStringEventArgs()
        {

        }

        public SimplPlusStringEventArgs(string value)
        {
            this.Value = value;
        }
    }
}
