using System;
using System.Collections.Generic;

namespace Spectrum.Logic.Models
{
    [Serializable]
    public class AddressNorthAmericaModel
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
        public virtual ICollection<OrganizationModel> Organizations { get; set; } // Many to many mapping
        public virtual ICollection<UserModel> Users { get; set; } // Many to many mapping

        public AddressNorthAmericaModel()
        {
            Organizations = new List<OrganizationModel>();
            Users = new List<UserModel>();
        }
    }

}
