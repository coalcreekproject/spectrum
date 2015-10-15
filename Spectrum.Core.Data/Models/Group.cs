using System.Collections.Generic;

namespace Spectrum.Core.Data.Models
{
    // Group

    public partial class Group
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name
        public int OrganizationId { get; set; } // OrganizationId
        public string Description { get; set; } // Description

        // Reverse navigation
        public virtual ICollection<User> Users { get; set; } // Many to many mapping

        // Foreign keys
        public virtual Organization Organization { get; set; } // FK_Group_Organization
        
        public Group()
        {
            Users = new List<User>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
