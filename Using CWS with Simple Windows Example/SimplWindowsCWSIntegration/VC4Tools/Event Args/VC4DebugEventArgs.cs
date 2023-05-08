using System;


namespace VC4Tools
{

    public delegate void VC4DebugEventHandler(VC4DebugEventArgs e);

    public class VC4DebugEventArgs : EventArgs
    {
        /// <summary>
        /// The debug message content.
        /// </summary>
        public string Message;
        /// <summary>
        /// The severity level of the debug message.
        /// </summary>
        public ushort Severity;

        /// <summary>
        /// Empty constructor for Simpl+ compatability.
        /// </summary>
        public VC4DebugEventArgs()
        {

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">The debug message to send to Simpl Windows.</param>
        /// <param name="severity">The severity of the debug message.</param>
        public VC4DebugEventArgs(string message, Severity severity)
        {
            this.Message = message;
            this.Severity = (ushort)severity;
        }
    }
}
