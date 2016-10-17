using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using NLog.Targets;
using NLog.Config;

namespace ProjectManager.Core
{
    public class NLogger : ProjectManager.Domain.ILogger
    {
        public Logger Log { get; private set; }

        public NLogger(string logFileName)
        {
            LoggingConfiguration config = new LoggingConfiguration();
            FileTarget target = new FileTarget { FileName = logFileName };
            config.AddTarget("file", target);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));
            LogManager.Configuration = config;
            Log = LogManager.GetLogger("ProjectManager");
        }

        public void LogInfo(string message)
        {
            Log.Info(message);
        }

        public void LogError(string message)
        {
            Log.Error(message);
        }

        /// <summary>
        /// Logs the passed message and exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        public void LogException(string message, Exception ex)
        {
            LogError(FormatException(message, ex));
        }


        private string FormatException(string message, Exception ex)
        {
            string errorMsg = string.Empty;

            if (message != null)
                errorMsg = message + (ex == null ? string.Empty : Environment.NewLine);

            if (ex != null)
                errorMsg += string.Format("Exception: {0}", ex.ToString()) + Environment.NewLine;

            return errorMsg;
        }
    }
}
