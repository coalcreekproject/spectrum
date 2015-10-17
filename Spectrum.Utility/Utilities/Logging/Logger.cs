using System;
using System.Configuration;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;

namespace Spectrum.Utility.Utilities.Logging
{
    public class Logger
    {
        private static Logger _log;

        public static Logger Log
        {
            get
            {
                return _log ?? (_log = new Logger());
            }
        }

        //TODO: Find an efficient way to have providers passed in.
        public static void SetupSemanticLoggingApplicationBlock()
        {
            //EventTracing setup
            string logConnString = ConfigurationManager.ConnectionStrings["LoggingConnString"].ToString();

            var sqlListener1 = SqlDatabaseLog.CreateListener("UI", logConnString);
            var sqlListener2 = SqlDatabaseLog.CreateListener("Infrastructure", logConnString);
            var sqlListener3 = SqlDatabaseLog.CreateListener("Service", logConnString);

            //get web.config value for logging level
            bool critcal = ConfigurationManager.AppSettings["Logging_LogCritical"].ToLower() == "true";
            bool error = ConfigurationManager.AppSettings["Logging_LogError"].ToLower() == "true";
            bool warning = ConfigurationManager.AppSettings["Logging_LogWarning"].ToLower() == "true";
            bool info = ConfigurationManager.AppSettings["Logging_LogInformation"].ToLower() == "true";

            //Enable the level of logging based on settings in web.config
            if (critcal)
            {
                sqlListener1.EnableEvents(LogWriter.Log, EventLevel.Critical);
                //uncomment if you add logging to your sub projects
                //sqlListener2.EnableEvents(MySolution.Infrastructure.InfrLogger.Log, EventLevel.Critical);
                //sqlListener3.EnableEvents(MySolution.Service.SvcLogger.Log, EventLevel.Critical);
            }
            if (error)
            {
                sqlListener1.EnableEvents(LogWriter.Log, EventLevel.Error);
                //uncomment if you add logging to your sub projects  
                //sqlListener2.EnableEvents(MySolution.Infrastructure.InfrLogger.Log, EventLevel.Error);
                //sqlListener3.EnableEvents(MySolution.Service.SvcLogger.Log, EventLevel.Error);
            }
            if (warning)
            {
                sqlListener1.EnableEvents(LogWriter.Log, EventLevel.Warning);
                //uncomment if you add logging to your sub projects                
                //sqlListener2.EnableEvents(MySolution.Infrastructure.InfrLogger.Log, EventLevel.Warning);
                //sqlListener3.EnableEvents(MySolution.Service.SvcLogger.Log, EventLevel.Warning);
            }
            if (info)
            {
                sqlListener1.EnableEvents(LogWriter.Log, EventLevel.Informational);
                //sqlListener2.EnableEvents(MySolution.Infrastructure.InfrLogger.Log, EventLevel.Informational);
                //sqlListener3.EnableEvents(MySolution.Service.SvcLogger.Log, EventLevel.Informational);
            }
        }

        public void Error(Exception ex)
        {
            string message = ex.Message;
            string innerMessage = "";
            if (ex.InnerException != null)
                innerMessage = ex.InnerException.Message;

            StackTrace st = new StackTrace();
            string methName = st.GetFrame(1).GetMethod().Name;
            string stack = st.ToString();
            LogWriter.Log.Error(message, innerMessage, methName, stack);

        }
        public void Error(Exception ex, string addedMessage)
        {
            string message = addedMessage + " :: " + ex.Message;
            string innerMessage = "";
            if (ex.InnerException != null)
                innerMessage = ex.InnerException.Message;

            StackTrace st = new StackTrace();
            string methName = st.GetFrame(1).GetMethod().Name;
            string stack = st.ToString();
            LogWriter.Log.Error(message, innerMessage, methName, stack);

        }
        public void Error(string message)
        {
            StackTrace st = new StackTrace();
            string methName = st.GetFrame(1).GetMethod().Name;
            string stack = st.ToString();
            LogWriter.Log.Error(message, "", methName, stack);
        }

        public void Critical(string message)
        {
            StackTrace st = new StackTrace();
            string methName = st.GetFrame(1).GetMethod().Name;
            string stack = st.ToString();
            LogWriter.Log.Critical(message, methName, stack);
        }

        public void Warning(string message)
        {
            StackTrace st = new StackTrace();
            string methName = st.GetFrame(1).GetMethod().Name;
            string stack = st.ToString();
            LogWriter.Log.Warning(message, methName, stack);
        }
        public void Information(string message)
        {
            StackTrace st = new StackTrace();
            string methName = st.GetFrame(1).GetMethod().Name;
            string stack = st.ToString();
            LogWriter.Log.Information(message, methName, stack);
        }
    }
}