namespace Spectrum.Data.Core.Models
{
    public partial class JurisdictionNote
    {
        public int Id { get; set; }
        public int JurisdictionId { get; set; }
        public string Note { get; set; }

        public virtual Jurisdiction Jurisdiction { get; set; }
    }
}