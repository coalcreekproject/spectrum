namespace Spectrum.Logic.Models
{
    public class JusrisdictionProfile
    {
        public int Id { get; set; } // Id (Primary key)
        public int JurisdictionId { get; set; } // JurisdictionId
        public string Description { get; set; } // Description

        // Foreign keys
        public virtual JurisdictionModel Jurisdiction { get; set; } // FK_JurisdictionProfile_Jurisdiction
    }
}
