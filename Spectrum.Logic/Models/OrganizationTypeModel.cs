using System;
using System.Collections.Generic;

namespace Spectrum.Logic.Models
{
    [Serializable]
    public class OrganizationTypeModel
    {
        public OrganizationTypeModel()
        {
            OrganizationModels = new List<OrganizationModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<OrganizationModel> OrganizationModels { get; set; }
    }
}