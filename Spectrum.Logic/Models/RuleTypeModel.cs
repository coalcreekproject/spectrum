using System;
using System.Collections.Generic;

namespace Spectrum.Logic.Models
{
    [Serializable]
    public class RuleTypeModel
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name
        public string Description { get; set; } // Description

        // Reverse navigation
        public virtual ICollection<RuleModel> Rules { get; set; } // Rule.FK_Rule_RuleType

        public RuleTypeModel()
        {
            Rules = new List<RuleModel>();
            
        }

        
    }

}
