using System;
using System.Collections.Generic;

namespace Spectrum.Logic.Models
{
    [Serializable]
    public class RoleModel
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name
        public int OrganizationId { get; set; } // OrganizationId
        public string Description { get; set; } // Description
        public int? ApplicationId { get; set; } // ApplicationId

        // Reverse navigation
        public virtual ICollection<UserModel> Users { get; set; } // Many to many mapping

        // Foreign keys
        public virtual ApplicationModel Application { get; set; } // FK_Role_Application
        public virtual OrganizationModel Organization { get; set; } // FK_Role_Organization
        
        public RoleModel()
        {
            Users = new List<UserModel>();
            
        }

        
    }

}
