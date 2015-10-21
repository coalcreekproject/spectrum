using System;
using System.Collections.Generic;

namespace Spectrum.Logic.Models
{
    [Serializable]
    public class JurisdictionModel
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name
        public int OrganizationId { get; set; } // OrganizationId

        // Reverse navigation
        public virtual ICollection<JusrisdictionProfile> JusrisdictionProfiles { get; set; }
        // JusrisdictionProfile.FK_JurisdictionProfile_Jurisdiction
        public virtual ICollection<UserModel> Users { get; set; } // Many to many mapping

        // Foreign keys
        public virtual OrganizationModel Organization { get; set; } // FK_Jurisdiction_Organization

        public JurisdictionModel()
        {
            JusrisdictionProfiles = new List<JusrisdictionProfile>();
            Users = new List<UserModel>();
        }
    }
}
