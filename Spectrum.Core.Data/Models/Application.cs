using System;
using System.Collections.Generic;

namespace Spectrum.Data.Core.Models
{
    public partial class Application
    {
        public Application()
        {
            ApplicationNotes = new List<ApplicationNote>();
            ApplicationParameters = new List<ApplicationParameter>();
            ApplicationProfiles = new List<ApplicationProfile>();
            OrganizationApplications = new List<OrganizationApplication>();
            Roles = new List<Role>();
            UserApplications = new List<UserApplication>();
            InitializePartial();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool? Cloaked { get; set; }
        public bool? Archive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedByUserId { get; set; }

        public virtual ICollection<ApplicationNote> ApplicationNotes { get; set; }
        public virtual ICollection<ApplicationParameter> ApplicationParameters { get; set; }
        public virtual ICollection<ApplicationProfile> ApplicationProfiles { get; set; }
        public virtual ICollection<OrganizationApplication> OrganizationApplications { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<UserApplication> UserApplications { get; set; }

        partial void InitializePartial();
    }
}