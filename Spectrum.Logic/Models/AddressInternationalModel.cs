using System;

namespace Spectrum.Logic.Models
{
    // AddressInternational
    [Serializable]
    public class AddressInternationalModel
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name
        public bool Default { get; set; } // Default
        public string Description { get; set; } // Description
        public string StreetOne { get; set; } // StreetOne
        public string StreetTwo { get; set; } // StreetTwo
        public string Country { get; set; } // Country
        public string City { get; set; } // City
        public string PoliticalBoundary { get; set; } // PoliticalBoundary
        public string PostalCode { get; set; } // Postal Code
    }

}
