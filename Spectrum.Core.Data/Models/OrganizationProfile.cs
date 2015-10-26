namespace Spectrum.Core.Data.Models
{
    // OrganizationProfile
    public partial class OrganizationProfile
    {
        public int Id { get; set; } // Id (Primary key)
        public int OrganizationId { get; set; } // OrganizationId
        public int? AddressNorthAmericaId { get; set; } // AddressNorthAmericaId
        public bool Default { get; set; } // Default
        public string ProfileName { get; set; } // ProfileName
        public string Description { get; set; } // Description
        public string PrimaryContact { get; set; } // PrimaryContact
        public string Phone { get; set; } // Phone
        public string Fax { get; set; } // Fax
        public string Email { get; set; } // Email
        public string Country { get; set; } // Country
        public string County { get; set; } // County
        public string TimeZone { get; set; } // TimeZone
        public bool? DstAdjust { get; set; } // DstAdjust
        public string Language { get; set; } // Language
        public string Notes { get; set; } // Notes

        // Foreign keys
        public virtual AddressNorthAmerica AddressNorthAmerica { get; set; } // FK_OrganizationProfile_AddressNorthAmerica
        public virtual Organization Organization { get; set; } // FK_OrganizationProfile_Organization
    }

}
