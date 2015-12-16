namespace Spectrum.Data.Core.Models
{
    public partial class JurisdictionProfile
    {
        public int Id { get; set; }
        public int JurisdictionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual Jurisdiction Jurisdiction { get; set; }
    }
}