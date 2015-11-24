using System;
using System.Collections.Generic;

namespace Spectrum.Logic.Models
{
    public partial class RuleModel
    {
        public RuleModel()
        {
            RuleParameterModels = new List<RuleParameterModel>();
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

        public virtual ICollection<RuleParameterModel> RuleParameterModels { get; set; }

        public virtual OrganizationModel OrganizationModel { get; set; }
        public virtual RuleTypeModel RuleTypeModel { get; set; }

        partial void InitializePartial();
    }
}