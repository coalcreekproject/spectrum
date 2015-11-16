using System;
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
        public bool? Cloaked { get; set; }
        public bool? Archive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedByUserId { get; set; }

        public virtual ICollection<RuleParameter> RuleParameters { get; set; }

        public virtual Organization Organization { get; set; }
        public virtual RuleType RuleType { get; set; }

        partial void InitializePartial();
    }
}