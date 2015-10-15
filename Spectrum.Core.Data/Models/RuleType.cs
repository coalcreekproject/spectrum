using System.Collections.Generic;

namespace Spectrum.Core.Data.Models
{
    // RuleType
    public partial class RuleType
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name
        public string Description { get; set; } // Description

        // Reverse navigation
        public virtual ICollection<Rule> Rules { get; set; } // Rule.FK_Rule_RuleType

        public RuleType()
        {
            Rules = new List<Rule>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
