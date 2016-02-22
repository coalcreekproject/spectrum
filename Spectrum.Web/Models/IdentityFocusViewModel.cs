using System.Collections.Generic;

namespace Spectrum.Web.Models
{
    public class IdentityFocusViewModel
    {
        public int UserId { get; set; }
        public int OrganizationId { get; set; }
        public int RoleId { get; set; }
        public int PositionId { get; set; }

        public List<OrganizationViewModel> OrganizationViewModels { get; set; }
        public List<RoleViewModel> RoleViewModels { get; set; }
        public List<PositionViewModel> PositionViewModels { get; set; }
    }
}