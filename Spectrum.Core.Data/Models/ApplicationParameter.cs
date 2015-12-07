namespace Spectrum.Core.Data.Models
{
    public partial class ApplicationParameter
    {
        public int? Id { get; set; }
        public int ApplicationId { get; set; }
        public int ParameterId { get; set; }

        public virtual Application Application { get; set; }
        public virtual Parameter Parameter { get; set; }
    }
}