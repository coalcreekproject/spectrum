using System;
using System.Collections.Generic;

namespace Spectrum.Logic.Models
{
    [Serializable]
    public class RuleTypeModel
    {
        public RuleTypeModel()
        {
            RuleModels = new List<RuleModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<RuleModel> RuleModels { get; set; }
    }
}