using System;
using System.Collections.Generic;

namespace Spectrum.Logic.Models
{
    [Serializable]
    public class GroupModel
    {
        public GroupModel()
        {
            UserModels = new List<UserModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int OrganizationId { get; set; }
        public string Description { get; set; }
        public bool? Cloaked { get; set; }
        public bool? Archive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedByUserId { get; set; }

        public virtual ICollection<UserModel> UserModels { get; set; }

        public virtual OrganizationModel OrganizationModel { get; set; }
    }
}