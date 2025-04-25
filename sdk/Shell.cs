//
// This autonomous intelligent system software is the property of Cartheur Research B.V. Copyright 2025, all rights reserved.
//
using System.Diagnostics;

namespace Cartheur.Animals.Robot
{
    /// <summary>
    /// A helper class for bash commands where any installed application can be executed, as long as it is supported by the application itself.
    /// </summary>
    public static class ShellHelper
    {
        public static Process ProcessObject { get; set; }
        public static string Result { get; set; }
        /// <summary>
        /// Sends the the specified application command to bash.
        /// </summary>
        /// <param name="application">The application to run in /bin/bash.</param>
        /// <param name="command">The command for the application to run.</param>
        /// <returns></returns>
        public static string Bash(this string application, string command)
        {
            ProcessObject = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = application,
                    Arguments = "\"" + command + "\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                }
            };

            ProcessObject.Start();
            Result = ProcessObject.StandardOutput.ReadToEnd();
            ProcessObject.WaitForExit();

            return Result;
        }
    }
}
