using System;
using System.Collections.Generic;

namespace Spectrum.Logic.Models
{
    [Serializable]
    public class OrganizationTypeModel
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name
        public string Description { get; set; } // Description

        // Reverse navigation
        public virtual ICollection<OrganizationModel> Organizations { get; set; } // Organization.FK_Organization_OrganizationType
        
        public OrganizationTypeModel()
        {
            Organizations = new List<OrganizationModel>();
            
        }

        
    }

}
