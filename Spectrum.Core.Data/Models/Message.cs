namespace Spectrum.Core.Data.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public bool? Urgent { get; set; }
        public string To { get; set; }
        public string ToEmail { get; set; }
        public string From { get; set; }
        public string FromEmail { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public string Body { get; set; }
        public string Encoding { get; set; }
        public string DisplayEncoding { get; set; }
        public string Relay { get; set; }
        public string RelayEmail { get; set; }
    }
}