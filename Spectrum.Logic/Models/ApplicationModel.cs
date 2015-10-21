using System;
using System.Collections.Generic;

namespace Spectrum.Logic.Models
{
    [Serializable]
    public class ApplicationModel
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name

        // Reverse navigation
        public virtual ICollection<ApplicationProfile> ApplicationProfiles { get; set; } // ApplicationProfile.FK_ApplicationDetail_Application
        public virtual ICollection<OrganizationApplicationModel> OrganizationApplications { get; set; } // Many to many mapping
        public virtual ICollection<ParameterModel> Parameters { get; set; } // Many to many mapping
        public virtual ICollection<RoleModel> Roles { get; set; } // Role.FK_Role_Application
        public virtual ICollection<UserApplicationModel> UserApplications { get; set; } // Many to many mapping

        public ApplicationModel()
        {
            ApplicationProfiles = new List<ApplicationProfile>();
            OrganizationApplications = new List<OrganizationApplicationModel>();
            Roles = new List<RoleModel>();
            UserApplications = new List<UserApplicationModel>();
            Parameters = new List<ParameterModel>();
        }
    }

}
