using System;

namespace Spectrum.Data.Eoc.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string EventTitle { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string EventEntry { get; set; }
    }
}