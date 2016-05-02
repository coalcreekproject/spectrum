using System;

namespace Spectrum.Data.Eoc.Models
{
    public class Log
    {
        public int LogId { get; set; }
        public string LogTitle { get; set; }
        public string LogName { get; set; }
        public DateTime LogDate { get; set; }
        public string LogEntry { get; set; }
    }
}