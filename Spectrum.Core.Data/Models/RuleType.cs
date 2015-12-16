using System.Collections.Generic;

namespace Spectrum.Data.Core.Models
{
    public partial class RuleType
    {
        public RuleType()
        {
            Rules = new List<Rule>();
            InitializePartial();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Rule> Rules { get; set; }

        partial void InitializePartial();
    }
}