using System.Collections.Generic;

namespace Spectrum.Logic.Models
{
    public partial class OrganizationTypeModel
    {
        public OrganizationTypeModel()
        {
            OrganizationModels = new List<OrganizationModel>();
            InitializePartial();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<OrganizationModel> OrganizationModels { get; set; }

        partial void InitializePartial();
    }
}