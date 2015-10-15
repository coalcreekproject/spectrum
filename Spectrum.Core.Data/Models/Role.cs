using System.Collections.Generic;

namespace Spectrum.Core.Data.Models
{
    // Role

    public partial class Role
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name
        public int OrganizationId { get; set; } // OrganizationId
        public string Description { get; set; } // Description
        public int? ApplicationId { get; set; } // ApplicationId

        // Reverse navigation
        public virtual ICollection<User> Users { get; set; } // Many to many mapping

        // Foreign keys
        public virtual Application Application { get; set; } // FK_Role_Application
        public virtual Organization Organization { get; set; } // FK_Role_Organization
        
        public Role()
        {
            Users = new List<User>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
