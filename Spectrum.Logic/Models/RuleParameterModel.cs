using System;

namespace Spectrum.Logic.Models
{
    public class RuleParameterModel
    {
        public int RuleId { get; set; }
        public int ParameterId { get; set; }
        public bool? Cloaked { get; set; }
        public bool? Archive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedByUserId { get; set; }

        public virtual ParameterModel ParameterModel { get; set; }
        public virtual RuleModel RuleModel { get; set; }
    }
}