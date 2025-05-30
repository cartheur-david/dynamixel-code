﻿//
// This autonomous intelligent system software is the property of Cartheur Research B.V. Copryright 2021 - 2025, all rights reserved.
//
using System.Text;

namespace Cartheur.Animals.Robot
{
    /// <summary>
    /// The class which performs logging for the library. Originated in MacOS 9.0.4 (via CodeWarrior in SheepShaver, September - December 2014).
    /// </summary>
    public static class Logging
    {
        private static bool _fileCreated = false;
        /// <summary>
        /// Default logging and transcripting if the setting is left blank in the config file.
        /// </summary>
        private static string LogModelName { get { return @"log"; } }
        private static string TranscriptModelName { get { return @"transcript"; } }
        /// <summary>
        /// The active configuration of the application.
        /// </summary>
        public static string ActiveConfiguration { get; set; }
        /// <summary>
        /// The type of model to use for logging.
        /// </summary>
        public static string LogModelFile { get; set; }
        /// <summary>
        /// The type of model to use for the transcript.
        /// </summary>
        public static string TranscriptModelFile { get; set; }
        /// <summary>
        /// The type of log to write.
        /// </summary>
        public enum LogType
        {
            /// <summary>
            /// The data log.
            /// </summary>
            Data,
            /// <summary>
            /// The error log.
            /// </summary>
            Error,
            /// <summary>
            /// The information log.
            /// </summary>
            Information,
            /// <summary>
            /// The warning log.
            /// </summary>
            Warning
        };
        /// <summary>
        /// The classes within the interpreter calling the log.
        /// </summary>
        public enum LogCaller
        {
            /// <summary>
            /// The external robot connection (puppeteering).
            /// </summary>
            ExternalRobotConnection,
            /// <summary>
            /// The file template.
            /// </summary>
            FileTemplate,
            /// <summary>
            /// The joi pose calibrator.
            /// </summary>
            JoiPose,
            /// <summary>
            /// Marshalling, as in calls to a C-library.
            /// </summary>
            Marshal,
            /// <summary>
            /// M.E.
            /// </summary>
            Me,
            /// <summary>
            /// Memory functions
            /// </summary>
            Memory,
            /// <summary>
            /// Motor control
            /// </summary>
            MotorControl,
            /// <summary>
            /// Voicing
            /// </summary>
            Voice
        }
        /// <summary>
        /// The last message passed to logging.
        /// </summary>
        public static string LastMessage = "";
        /// <summary>
        /// The delegate for returning the last log message to the calling application.
        /// </summary>
        public delegate void LoggingDelegate();
        /// <summary>
        /// Occurs when [returned to console] is called.
        /// </summary>
        public static event LoggingDelegate ReturnedToConsole;
        /// <summary>
        /// Optional means to model the logfile from its original "logfile" model.
        /// </summary>
        /// <param name="modelName"></param>
        /// <returns>The path for the logfile.</returns>
        public static void ChangeLogModel(string modelName)
        {
            LogModelFile = modelName;
        }

        /// <summary>
        /// Logs a message sent from the calling application to a file.
        /// </summary>
        /// <param name="message">The message to log. Space between the message and log type enumeration provided.</param>
        /// <param name="logType">Type of the log.</param>
        /// <param name="caller">The class creating the log entry.</param>
        public static void WriteLog(string message, LogType logType, LogCaller caller)
        {
            if (LogModelFile == null)
            {
                LogModelFile = LogModelName;
            }
            LastMessage = message;
            StreamWriter stream = new StreamWriter(Path.Combine(Environment.CurrentDirectory, "logs", LogModelFile + @".txt"), true);
            switch (logType)
            {
                case LogType.Data:
                    stream.WriteLine(message);
                    break;
                case LogType.Error:
                    stream.WriteLine(DateTime.Now + " - " + " ERROR " + " - " + message + " from class " + caller + ".");
                    break;
                case LogType.Warning:
                    stream.WriteLine(DateTime.Now + " - " + " WARNING " + " - " + message + " from class " + caller + ".");
                    break;
                case LogType.Information:
                    stream.WriteLine(DateTime.Now + " - " + message + ". This was called from the class " + caller + ".");
                    break;
            }
            stream.Close();
            if (!Equals(null, ReturnedToConsole))
            {
                ReturnedToConsole();
            }
        }
        /// <summary>
        /// Logs a message sent from the calling application to a file.
        /// </summary>
        /// <param name="message">The message to log. Space between the message and log type enumeration provided.</param>
        /// <param name="logType">Type of the log.</param>
        /// <param name="caller">The class creating the log entry.</param>
        /// <param name="LogModelFile">When using a customized filename.</param>
        public static void WriteLog(string message, LogType logType, LogCaller caller, string LogModelFile)
        {
            if (LogModelFile == "")
            {
                LogModelFile = LogModelName;
            }
            LastMessage = message;
            StreamWriter stream = new StreamWriter(Path.Combine(Environment.CurrentDirectory, "logs", LogModelFile + @".txt"), true);
            switch (logType)
            {
                case LogType.Data:
                    stream.WriteLine(message);
                    break;
                case LogType.Error:
                    stream.WriteLine(DateTime.Now + " - " + " ERROR " + " - " + message + " from class " + caller + ".");
                    break;
                case LogType.Warning:
                    stream.WriteLine(DateTime.Now + " - " + " WARNING " + " - " + message + " from class " + caller + ".");
                    break;
                case LogType.Information:
                    stream.WriteLine(DateTime.Now + " - " + message + ". This was called from the class " + caller + ".");
                    break;
            }
            stream.Close();
            if (!Equals(null, ReturnedToConsole))
            {
                ReturnedToConsole();
            }
        }
        /// <summary>
        /// Records a transcript of the conversation.
        /// </summary>
        /// <param name="message">The message to save in transcript format.</param>
        /// <param name="insertNewLine">Inserts a new line, if required.</param>
        /// <param name="fileNumber">Use for saving iterative transcript files.</param>
        public static void RecordTranscript(string message, int fileNumber, bool insertNewLine = false)
        {
            try
            {
                StreamWriter stream = new StreamWriter(Path.Combine(Environment.CurrentDirectory, "logs", TranscriptModelFile + @".txt"), true);
                if (!_fileCreated && fileNumber == 0)
                {
                    // File has not been created previously, write the header to the file.
                    stream.WriteLine(@"August 2017 Template" + Environment.NewLine + @"A transcript log for an interative conversation between two aeons, in pursuit of validation / critique of a paper as well as outlining an example of ethical application.");
                    stream.WriteLine(Environment.NewLine);
                    _fileCreated = true;
                }
                if (fileNumber != 0)
                {
                    stream.Dispose();
                    stream = new StreamWriter(Path.Combine(Environment.CurrentDirectory, "logs", TranscriptModelFile + fileNumber + @".txt"), true);
                    if (!_fileCreated)
                    {
                        stream.WriteLine(@"August 2017 Template" + Environment.NewLine + @"A transcript log for an interative conversation between two aeons, in pursuit of validation / critique of a paper as well as outlining an example of ethical application.");
                        stream.WriteLine(Environment.NewLine);
                        _fileCreated = true;
                    }
                }
                if (insertNewLine)
                    stream.WriteLine(Environment.NewLine);
                stream.WriteLine(message);
                stream.Close();
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message, LogType.Error, LogCaller.Me);
            }

        }
        /// <summary>
        /// Records a transcript of the conversation.
        /// </summary>
        /// <param name="message">The message to save in transcript format.</param>
        public static void RecordTranscript(string message)
        {
            if (TranscriptModelFile == "")
            {
                TranscriptModelFile = TranscriptModelName;
            }
            try
            {
                StreamWriter stream = new StreamWriter(Path.Combine(Environment.CurrentDirectory, "logs", TranscriptModelFile + @".txt"), true);
                stream.WriteLine(DateTime.Now + " - " + message);
                stream.Close();
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message, LogType.Error, LogCaller.Me);
            }

        }
        /// <summary>
        /// Saves the last result to support analysis of the algorithm.
        /// </summary>
        /// <param name="output">The output from the conversation.</param>
        public static void SaveLastResult(StringBuilder output)
        {
            try
            {
                StreamWriter stream = new StreamWriter(Path.Combine(Environment.CurrentDirectory, "db", "analytics" + @".txt"), true);
                stream.WriteLine(DateTime.Now + " - " + output);
                stream.Close();
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message, LogType.Error, LogCaller.Me);
            }

        }
        /// <summary>
        /// Saves the last result to support analysis of the algorithm to storage.
        /// </summary>
        /// <param name="output">The output from the conversation.</param>
        public static void SaveLastResultToStorage(StringBuilder output)
        {
            try
            {
                StreamWriter stream = new StreamWriter(Path.Combine(Environment.CurrentDirectory, "db", "analyticsStorage" + @".txt"), true);
                stream.WriteLine("#" + DateTime.Now + ";" + output);
                stream.Close();
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message, LogType.Error, LogCaller.Me);
            }

        }
        /// <summary>
        /// Writes a debug log with object parameters.
        /// </summary>
        /// <param name="objects">The objects.</param>
        public static void Debug(params object[] objects)
        {
            StreamWriter stream = new StreamWriter(Path.Combine(Environment.CurrentDirectory, "logs", "debugdump" + @".txt"), true);
            foreach (object obj in objects)
            {
                stream.WriteLine(obj);
            }
            stream.WriteLine("--");
            stream.Close();
        }
    }
}
