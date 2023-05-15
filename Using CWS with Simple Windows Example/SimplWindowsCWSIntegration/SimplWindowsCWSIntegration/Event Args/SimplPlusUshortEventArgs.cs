using System;

namespace SimplWindowsCWSIntegration
{
    /// <summary>
    /// Delegate to provide a Simpl+ compatible event encapsulating SimplPlusUshortEventArgs.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void SimplPlusUshortEventHandler(object sender, SimplPlusUshortEventArgs e);

    /// <summary>
    /// Event Args for passing ushort values back to Simpl+.
    /// </summary>
    public class SimplPlusUshortEventArgs : EventArgs
    {
        public ushort Value;

        /// <summary>
        /// Empty constructor for Simpl+ compatability.
        /// </summary>
        public SimplPlusUshortEventArgs()
        {

        }

        public SimplPlusUshortEventArgs(ushort value)
        {
            this.Value = value;
        }
    }
}