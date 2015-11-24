using System.Collections.Generic;

namespace Spectrum.Logic.Models
{
    public partial class RuleTypeModel
    {
        public RuleTypeModel()
        {
            RuleModels = new List<RuleModel>();
            InitializePartial();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<RuleModel> RuleModels { get; set; }

        partial void InitializePartial();
    }
}