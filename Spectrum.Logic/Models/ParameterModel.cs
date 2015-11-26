using System;
using System.Collections.Generic;

namespace Spectrum.Logic.Models
{
    [Serializable]
    public class ParameterModel
    {
        public ParameterModel()
        {
            ApplicationParameterModels = new List<ApplicationParameterModel>();
            RuleParameterModels = new List<RuleParameterModel>();
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

        public virtual ICollection<ApplicationParameterModel> ApplicationParameterModels { get; set; }
        public virtual ICollection<RuleParameterModel> RuleParameterModels { get; set; }
    }
}