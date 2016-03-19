using System.Collections.Generic;
using Spectrum.Data.Core.Models;

namespace Spectrum.Web.Models
{
    public class OrganizationViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? OrganizationTypeId { get; set; }
        public string OrganizationTypeName { get; set; }
        public virtual OrganizationType OrganizationType { get; set; }
        public List<OrganizationTypeViewModel> OrganizationTypes { get; set; }
    }
}