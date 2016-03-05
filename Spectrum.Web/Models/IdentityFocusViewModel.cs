using System.Collections.Generic;

namespace Spectrum.Web.Models
{
    public class IdentityFocusViewModel
    {
        public int UserId { set; get; }
        public int SelectedOrganizationId { get; set; }
        public string SelectedOrganizationName { get; set; }
        public int SelectedRoleId { get; set; }
        public string SelectedRoleName { get; set; }
        public int SelectedPositionId { get; set; }
        public string SelectedPositionName { get; set; }
    }
}