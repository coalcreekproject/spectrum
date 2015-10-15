using System.Collections.Generic;

namespace Spectrum.Core.Data.Models
{
    // Parameter
    public partial class Parameter
    {
        public int Id { get; set; } // Id (Primary key)
        public string Key { get; set; } // Key
        public string Value { get; set; } // Value

        // Reverse navigation
        public virtual ICollection<Application> Applications { get; set; } // Many to many mapping
        public virtual ICollection<Rule> Rules { get; set; } // Many to many mapping

        public Parameter()
        {
            Applications = new List<Application>();
            Rules = new List<Rule>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
