using System;
using System.IO;
using System.Web.Hosting;

namespace MyFigureCollection
{
    /// <summary>
    /// Creates a log text for exception handling
    /// Currently out of commision
    /// </summary>
    public static class Logger
    {
        private static readonly object LogObject = new object();

        static readonly TextWriter tw;

        static Logger()
        {
            tw = TextWriter.Synchronized(File.AppendText(HostingEnvironment.MapPath("/Log.txt") ?? "Log.txt"));
        }


        /// <summary>
        /// Handles IOExpception if parsed in correctly
        /// </summary>
        /// <param name="logMessage">Text to write</param>
        public static void Write(string logMessage)
        {
            try
            {
                Log(logMessage, tw);
            }
            catch (IOException e)
            {
                tw.Close();
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Writes text to file
        /// </summary>
        /// <param name="logMessage">Text to write</param>
        /// <param name="w">Textwriter object</param>
        private static void Log(string logMessage, TextWriter w)
        {
            lock (LogObject)
            {
                w.WriteLine("[{0}]: {1}", DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), logMessage);
                w.Flush();
            }
        }
    }
}
