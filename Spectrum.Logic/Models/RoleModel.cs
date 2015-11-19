using System;
using System.Collections.Generic;

namespace Spectrum.Logic.Models
{
    [Serializable]
    public class RoleModel
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ApplicationId { get; set; }
        public bool? Cloaked { get; set; }
        public bool? Archive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedByUserId { get; set; }

        public virtual ICollection<UserModel> Users { get; set; }

        public virtual ApplicationModel Application { get; set; }
        public virtual OrganizationModel Organization { get; set; }

        public RoleModel()
        {
            Users = new List<UserModel>();
        }
    }
}
