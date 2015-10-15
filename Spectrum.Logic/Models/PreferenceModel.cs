using System.Collections.Generic;

namespace Spectrum.Logic.Models
{
    // Preference

    public class PreferenceModel
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name
        public string Description { get; set; } // Description
        public string Value { get; set; } // Value

        // Reverse navigation
        public virtual ICollection<OrganizationModel> Organizations { get; set; } // Many to many mapping
        public virtual ICollection<UserModel> Users { get; set; } // Many to many mapping

        public PreferenceModel()
        {
            Organizations = new List<OrganizationModel>();
            Users = new List<UserModel>();
            
        }

        
    }

}
