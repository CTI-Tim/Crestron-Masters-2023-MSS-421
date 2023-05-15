using System;

namespace SimplWindowsCWSIntegration
{
    /// <summary>
    /// Delegate to provide a Simpl+ compatible event encapsulating SimplPlusStringEventArgs
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void SimplPlusStringEventHandler(object sender, SimplPlusStringEventArgs e);

    /// <summary>
    /// Event Args for passing string values back to Simpl+.
    /// </summary>
    public class SimplPlusStringEventArgs : EventArgs
    {

        public string Value;

        /// <summary>
        /// Empty constructor for Simpl+ compatability.
        /// </summary>
        public SimplPlusStringEventArgs()
        {

        }

        public SimplPlusStringEventArgs(string value)
        {
            this.Value = value;
        }
    }
}
