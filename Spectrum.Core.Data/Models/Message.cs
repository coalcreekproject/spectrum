using System;

namespace Spectrum.Core.Data.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public bool? Urgent { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Body { get; set; }
        public string EncodingType { get; set; }
        public string EmailRelay { get; set; }
        public bool? Cloaked { get; set; }
        public bool? Archive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedByUserId { get; set; }
    }
}