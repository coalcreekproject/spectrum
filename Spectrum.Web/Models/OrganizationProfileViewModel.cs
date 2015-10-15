namespace Spectrum.Web.Models
{
    public class OrganizationProfileViewModel
    {
        public int Id { get; set; } // Id (Primary key)
        public int OrganizationId { get; set; } // OrganizationId
        public string ProfileName { get; set; } // Name
        public string Description { get; set; } // Description
        public string StreetAddressOne { get; set; } // StreetAddressOne
        public string StreetAddressTwo { get; set; } // StreetAddressTwo
        public string City { get; set; } // City
        public string State { get; set; } // State
        public string Zip { get; set; } // Zip
        public string Phone { get; set; } // Phone
        public string Fax { get; set; } // Fax
        public bool? Default { get; set; } // Default
        public string Email { get; set; } // Email
        public string Country { get; set; } // Country
        public string County { get; set; } // County
        public string TimeZone { get; set; } // TimeZone
        public bool? DstAdjust { get; set; } // DstAdjust
        public string Language { get; set; } // Language
    }
}