using System;

namespace Spectrum.Logic.Models
{
    [Serializable]
    public class UserRoleModel
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int OrganizationId { get; set; }
        public bool? Default { get; set; }
        public virtual OrganizationModel Organization { get; set; }
        public virtual RoleModel Role { get; set; }
    }
}