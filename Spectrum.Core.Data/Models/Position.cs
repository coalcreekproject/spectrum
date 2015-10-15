using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Core.Data.Models
{
    public partial class Position
    {
        public int Id { get; set; } // Id (Primary key)
        public string OrganizationId { get; set; } // OrganizationId
        public string Name { get; set; } // Name
        public string Description { get; set; } // Description
        public string Value { get; set; } // Value

        // Reverse navigation
        public virtual ICollection<User> Users { get; set; } // Many to many mapping

        public Position()
        {
            Users = new List<User>();
            InitializePartial();
        }

        partial void InitializePartial();
    }
}
