using System.Diagnostics.Tracing;

namespace Spectrum.Utility.Utilities.Logging
{
    public class LogWriter : EventSource
    {
        public static readonly LogWriter Log = new LogWriter();

        [Event(1000, Message = "{0}", Level = EventLevel.Critical)]
        public void Critical(string message, string method, string stack)
        {
            object[] parms = new object[] {message, method, stack};
            if (IsEnabled())
                WriteEvent(1000, parms);
        }

        [Event(1001, Message = "{0}", Level = EventLevel.Error)]
        public void Error(string message, string innerExceptionMessage, string method, string stack)
        {
            object[] parms = new object[] { message, innerExceptionMessage, method, stack };
            if (IsEnabled())
                WriteEvent(1001, parms);
        }

        [Event(1002, Message = "{0}", Level = EventLevel.Warning)]
        public void Warning(string message, string method, string stack)
        {
            object[] parms = new object[] { message, method, stack };
            if (IsEnabled())
                WriteEvent(1002, parms);
        }

        [Event(1003, Message = "{0}", Level = EventLevel.Informational)]
        public void Information(string message, string method, string stack)
        {
            object[] parms = new object[] { message, method, stack };
            if (IsEnabled())
                WriteEvent(1003, parms);
        }
    }
}