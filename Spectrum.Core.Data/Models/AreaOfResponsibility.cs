using System;
using System.Collections.Generic;

namespace Spectrum.Core.Data.Models
{
    public partial class AreaOfResponsibility
    {
        public AreaOfResponsibility()
        {
            AreaOfResponsibilityNotes = new List<AreaOfResponsibilityNote>();
            AreaOfResponsibilityProfiles = new List<AreaOfResponsibilityProfile>();
            UserAreaOfResponsibilities = new List<UserAreaOfResponsibility>();
            InitializePartial();
        }

        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Cloaked { get; set; }
        public bool? Archive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedByUserId { get; set; }

        public virtual ICollection<AreaOfResponsibilityNote> AreaOfResponsibilityNotes { get; set; }
        public virtual ICollection<AreaOfResponsibilityProfile> AreaOfResponsibilityProfiles { get; set; }
        public virtual ICollection<UserAreaOfResponsibility> UserAreaOfResponsibilities { get; set; }

        public virtual Organization Organization { get; set; }

        partial void InitializePartial();
    }
}