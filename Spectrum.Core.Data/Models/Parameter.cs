using System;
using System.Collections.Generic;

namespace Spectrum.Data.Core.Models
{
    public partial class Parameter
    {
        public Parameter()
        {
            ApplicationParameters = new List<ApplicationParameter>();
            RuleParameters = new List<RuleParameter>();
            InitializePartial();
        }

        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public bool? Cloaked { get; set; }
        public bool? Archive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedByUserId { get; set; }

        public virtual ICollection<ApplicationParameter> ApplicationParameters { get; set; }
        public virtual ICollection<RuleParameter> RuleParameters { get; set; }

        partial void InitializePartial();
    }
}