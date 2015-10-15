using System.Collections.Generic;

namespace Spectrum.Core.Data.Models
{
    // OrganizationType

    public partial class OrganizationType
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name
        public string Description { get; set; } // Description

        // Reverse navigation
        public virtual ICollection<Organization> Organizations { get; set; } // Organization.FK_Organization_OrganizationType
        
        public OrganizationType()
        {
            Organizations = new List<Organization>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
