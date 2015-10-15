using System.Collections.Generic;

namespace Spectrum.Core.Data.Models
{
    // AreaOfResponsibility

    public partial class AreaOfResponsibility
    {
        public int Id { get; set; } // Id (Primary key)
        public int OrganizationId { get; set; } // OrganizationId
        public string Name { get; set; } // Name
        public string Description { get; set; } // Description

        // Reverse navigation
        public virtual ICollection<User> Users { get; set; } // Many to many mapping

        // Foreign keys
        public virtual Organization Organization { get; set; } // FK_AreaOfResponsibility_OrganizationId
        
        public AreaOfResponsibility()
        {
            Users = new List<User>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
