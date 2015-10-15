using System.Collections.Generic;

namespace Spectrum.Core.Data.Models
{
    // Jurisdiction

    public partial class Jurisdiction
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name
        public int OrganizationId { get; set; } // OrganizationId

        // Reverse navigation
        public virtual ICollection<JusrisdictionProfile> JusrisdictionProfiles { get; set; } // JusrisdictionProfile.FK_JurisdictionProfile_Jurisdiction
        public virtual ICollection<User> Users { get; set; } // Many to many mapping

        // Foreign keys
        public virtual Organization Organization { get; set; } // FK_Jurisdiction_Organization

        public Jurisdiction()
        {
            JusrisdictionProfiles = new List<JusrisdictionProfile>();
            Users = new List<User>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
