using System;
using System.Collections.Generic;

namespace Spectrum.Core.Data.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new List<User>();
            InitializePartial();
        }

        public int OrganizationId { get; set; }
        public string Description { get; set; }
        public int? ApplicationId { get; set; }
        public bool? Cloaked { get; set; }
        public bool? Archive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedByUserId { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual Application Application { get; set; }
        public virtual Organization Organization { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

        partial void InitializePartial();
    }
}