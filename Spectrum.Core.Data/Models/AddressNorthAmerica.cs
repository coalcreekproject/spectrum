using System.Collections.Generic;

namespace Spectrum.Core.Data.Models
{
    // AddressNorthAmerica

    public partial class AddressNorthAmerica
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name
        public bool Default { get; set; } // Default
        public string Description { get; set; } // Description
        public string StreetOne { get; set; } // StreetOne
        public string StreetTwo { get; set; } // StreetTwo
        public string City { get; set; } // City
        public string State { get; set; } // State
        public string Zip { get; set; } // Zip

        // Reverse navigation
        public virtual ICollection<Organization> Organizations { get; set; } // Many to many mapping
        public virtual ICollection<User> Users { get; set; } // Many to many mapping

        public AddressNorthAmerica()
        {
            Organizations = new List<Organization>();
            Users = new List<User>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
