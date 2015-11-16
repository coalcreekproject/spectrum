using System;
using System.Collections.Generic;

namespace Spectrum.Core.Data.Models
{
    public partial class Group
    {
        public Group()
        {
            Users = new List<User>();
            InitializePartial();
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

        public virtual ICollection<User> Users { get; set; }

        public virtual Organization Organization { get; set; }

        partial void InitializePartial();
    }
}