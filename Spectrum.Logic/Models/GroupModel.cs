using System;
using System.Collections.Generic;

namespace Spectrum.Logic.Models
{
    [Serializable]
    public class GroupModel
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name
        public int OrganizationId { get; set; } // OrganizationId
        public string Description { get; set; } // Description

        // Reverse navigation
        public virtual ICollection<UserModel> Users { get; set; } // Many to many mapping

        // Foreign keys
        public virtual OrganizationModel Organization { get; set; } // FK_Group_Organization
        
        public GroupModel()
        {
            Users = new List<UserModel>();
        }
    }
}
