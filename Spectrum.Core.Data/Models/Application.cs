using System.Collections.Generic;

namespace Spectrum.Core.Data.Models
{
    // Application

    public partial class Application
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name

        // Reverse navigation
        public virtual ICollection<ApplicationProfile> ApplicationProfiles { get; set; } // ApplicationProfile.FK_ApplicationDetail_Application
        public virtual ICollection<OrganizationApplication> OrganizationApplications { get; set; } // Many to many mapping
        public virtual ICollection<Parameter> Parameters { get; set; } // Many to many mapping
        public virtual ICollection<Role> Roles { get; set; } // Role.FK_Role_Application
        public virtual ICollection<UserApplication> UserApplications { get; set; } // Many to many mapping

        public Application()
        {
            ApplicationProfiles = new List<ApplicationProfile>();
            OrganizationApplications = new List<OrganizationApplication>();
            Roles = new List<Role>();
            UserApplications = new List<UserApplication>();
            Parameters = new List<Parameter>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
