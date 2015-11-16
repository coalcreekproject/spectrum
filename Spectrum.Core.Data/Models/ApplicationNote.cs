namespace Spectrum.Core.Data.Models
{
    public class ApplicationNote
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string Note { get; set; }

        public virtual Application Application { get; set; }
    }
}