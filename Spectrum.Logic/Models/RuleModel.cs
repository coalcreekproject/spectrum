using System.Collections.Generic;

namespace Spectrum.Logic.Models
{
    // Rule
    public class RuleModel
    {
        public int Id { get; set; } // Id (Primary key)
        public int OrganizationId { get; set; } // OrganizationId
        public string Name { get; set; } // Name
        public string Description { get; set; } // Description
        public int RuleTypeId { get; set; } // RuleTypeId

        // Reverse navigation
        public virtual ICollection<ParameterModel> Parameters { get; set; } // Many to many mapping

        // Foreign keys
        public virtual OrganizationModel Organization { get; set; } // FK_Rule_Organization
        public virtual RuleTypeModel RuleType { get; set; } // FK_Rule_RuleType

        public RuleModel()
        {
            Parameters = new List<ParameterModel>();
            
        }

        
    }

}
