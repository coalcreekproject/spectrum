using System;
using System.Collections.Generic;

namespace Spectrum.Logic.Models
{
    [Serializable]
    public class RoleModel
    {
        public RoleModel()
        {
            UserRoles = new List<UserRoleModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int OrganizationId { get; set; }
        public string Description { get; set; }
        public int? ApplicationId { get; set; }

        public virtual ICollection<UserRoleModel> UserRoles { get; set; }

        public virtual OrganizationModel Organization { get; set; }
    }
}