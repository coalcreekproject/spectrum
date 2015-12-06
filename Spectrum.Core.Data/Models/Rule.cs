using System.Collections.Generic;

namespace Spectrum.Core.Data.Models
{
    public partial class Rule
    {
        public Rule()
        {
            RuleParameters = new List<RuleParameter>();
            InitializePartial();
        }

        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RuleTypeId { get; set; }

        public virtual ICollection<RuleParameter> RuleParameters { get; set; }

        public virtual Organization Organization { get; set; }
        public virtual RuleType RuleType { get; set; }

        partial void InitializePartial();
    }
}