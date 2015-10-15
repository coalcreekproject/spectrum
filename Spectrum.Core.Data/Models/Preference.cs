using System.Collections.Generic;

namespace Spectrum.Core.Data.Models
{
    // Preference

    public partial class Preference
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name
        public string Description { get; set; } // Description
        public string Value { get; set; } // Value

        // Reverse navigation
        public virtual ICollection<Organization> Organizations { get; set; } // Many to many mapping
        public virtual ICollection<User> Users { get; set; } // Many to many mapping

        public Preference()
        {
            Organizations = new List<Organization>();
            Users = new List<User>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
