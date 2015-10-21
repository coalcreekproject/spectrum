using System;
using System.Collections.Generic;

namespace Spectrum.Logic.Models
{
    [Serializable]
    public class PositionModel
    {
        public int Id { get; set; } // Id (Primary key)
        public string OrganizationId { get; set; } // OrganizationId
        public string Name { get; set; } // Name
        public string Description { get; set; } // Description
        public string Value { get; set; } // Value

        // Reverse navigation
        public virtual ICollection<UserModel> Users { get; set; } // Many to many mapping

        public PositionModel()
        {
            Users = new List<UserModel>();
            
        }

        
    }
}
