using Crestron.SimplSharp;
using System;
using System.Collections.Generic;

namespace VC4Tools
{
    public static class VC4Debugger
    {
        private static readonly Dictionary<string,List<Action<string>>> commands=new Dictionary<string,List<Action<string>>>();

        #region Event Handlers
        /// <summary>
        /// An error message is ready to be despatched to Simpl Windows. 
        /// </summary>
        public static event VC4DebugEventHandler ErrorMessage;
        #endregion


        #region Public Methods
        /// <summary>
        /// Processes the command from 
        /// </summary>
        /// <param name="command">The command to process</param>
        public static void ProcessCommand(string command)
        {
            try
            {
                string[] result = command.Split(new[] { ' ' }, 2);
                if (commands.TryGetValue(result[0].ToLower(), out List<Action<string>> actions))
                    foreach (var action in actions)
                        action(result.Length > 1 ? result[1] : String.Empty);
                else
                    Debug("ERROR: Bad or Incomplete Command");
            }
            catch (Exception ex)
            {
                Exception("VC4Debugger - ProcessConsoleCommand(command)", ex);
                ErrorLog.Exception("VC4Debugger - ProcessConsoleCommand(command)", ex);
            }
        }

        public static void AddNewCommand(Action<string> action, string command)
        {
            var lowerCaseCommand = command.ToLower();
            if (commands.ContainsKey(lowerCaseCommand))
                commands[lowerCaseCommand].Add(action);
            else
                commands[lowerCaseCommand] = new List<Action<string>>() { action };
        }

        public static void Debug(string message, params object[] parameters) => OnErrorMessage(Severity.Debug, message, parameters);

        public static void Debug(string message) => OnErrorMessage(Severity.Debug, message);

        public static void Notice(string message, params object[] parameters) => OnErrorMessage(Severity.Notice, message, parameters);

        public static void Notice(string message) => OnErrorMessage(Severity.Notice, message);

        public static void Warning(string message, params object[] parameters) => OnErrorMessage(Severity.Warning, message, parameters);

        public static void Warning(string message) => OnErrorMessage(Severity.Warning, message);

        public static void Error(string message, params object[] parameters) => OnErrorMessage(Severity.Error, message, parameters);

        public static void Error(string message) => OnErrorMessage(Severity.Error, message);

        public static void Exception(string message, params object[] parameters) => OnErrorMessage(Severity.Exception, message, parameters);

        public static void Exception(Exception ex) => OnErrorMessage(Severity.Exception, ex.Message);
        public static void Exception(string message) => OnErrorMessage(Severity.Exception, message);
        #endregion

        #region Private Event Handling
        private static void OnErrorMessage(Severity severity, string message, params object[] parameters)
        {
            try
            {
                OnErrorMessage(severity, string.Format(message, parameters));
            }
            catch (Exception ex)
            {
                ErrorLog.Exception("VC4Debugger - Message(severity, string, params object[])", ex);
            }
        }

        private static void OnErrorMessage(Severity severity, string message) => ErrorMessage?.Invoke(new VC4DebugEventArgs(message, severity));
        #endregion
    }
}