namespace Spectrum.Core.Data.Models
{
    public partial class JusrisdictionProfile
    {
        public int Id { get; set; } // Id (Primary key)
        public int JurisdictionId { get; set; } // JurisdictionId
        public string Description { get; set; } // Description

        // Foreign keys
        public virtual Jurisdiction Jurisdiction { get; set; } // FK_JurisdictionProfile_Jurisdiction
    }
}
