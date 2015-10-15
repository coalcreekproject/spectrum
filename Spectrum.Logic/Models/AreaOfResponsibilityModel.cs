using System.Collections.Generic;

namespace Spectrum.Logic.Models
{
    // AreaOfResponsibility

    public class AreaOfResponsibilityModel
    {
        public int Id { get; set; } // Id (Primary key)
        public int OrganizationId { get; set; } // OrganizationId
        public string Name { get; set; } // Name
        public string Description { get; set; } // Description

        // Reverse navigation
        public virtual ICollection<UserModel> Users { get; set; } // Many to many mapping

        // Foreign keys
        public virtual OrganizationModel Organization { get; set; } // FK_AreaOfResponsibility_OrganizationId

        public AreaOfResponsibilityModel()
        {
            Users = new List<UserModel>();
        }
    }
}
