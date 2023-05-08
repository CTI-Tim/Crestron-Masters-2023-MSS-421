using System;

namespace SimplWindowsCWSKeypad
{
    /// <summary>
    /// Simpl+ compatable delegate for publishing events.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void SimplPlusButtonEventHandler(object sender, SimplPlusButtonEventArgs e);

    public class SimplPlusButtonEventArgs : EventArgs
    {
        /// <summary>
        /// The index of the button to be pressed.
        /// </summary>
        public ushort Index;

        /// <summary>
        /// Empty constructor for Simpl+ compatability.
        /// </summary>
        public SimplPlusButtonEventArgs()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="index">The index of the button to press.</param>
        public SimplPlusButtonEventArgs(ushort index)
        {
            // Store the button index.
            this.Index = index;

        }
    }
}
