using System.Collections.Generic;

namespace Spectrum.Core.Data.Models
{
    // Rule
    public partial class Rule
    {
        public int Id { get; set; } // Id (Primary key)
        public int OrganizationId { get; set; } // OrganizationId
        public string Name { get; set; } // Name
        public string Description { get; set; } // Description
        public int RuleTypeId { get; set; } // RuleTypeId

        // Reverse navigation
        public virtual ICollection<Parameter> Parameters { get; set; } // Many to many mapping

        // Foreign keys
        public virtual Organization Organization { get; set; } // FK_Rule_Organization
        public virtual RuleType RuleType { get; set; } // FK_Rule_RuleType

        public Rule()
        {
            Parameters = new List<Parameter>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
