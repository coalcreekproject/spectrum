using System;
using System.Collections.Generic;

namespace Spectrum.Logic.Models
{
    [Serializable]
    public class ParameterModel
    {
        public int Id { get; set; } // Id (Primary key)
        public string Key { get; set; } // Key
        public string Value { get; set; } // Value

        // Reverse navigation
        public virtual ICollection<ApplicationModel> Applications { get; set; } // Many to many mapping
        public virtual ICollection<RuleModel> Rules { get; set; } // Many to many mapping

        public ParameterModel()
        {
            Applications = new List<ApplicationModel>();
            Rules = new List<RuleModel>();
            
        }

        
    }

}
