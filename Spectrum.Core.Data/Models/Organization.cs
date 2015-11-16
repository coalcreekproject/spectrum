using System;
using System.Collections.Generic;

namespace Spectrum.Core.Data.Models
{
    public partial class Organization
    {
        public Organization()
        {
            AreaOfResponsibilities = new List<AreaOfResponsibility>();
            Groups = new List<Group>();
            Jurisdictions = new List<Jurisdiction>();
            OrganizationApplications = new List<OrganizationApplication>();
            OrganizationNotes = new List<OrganizationNote>();
            OrganizationProfiles = new List<OrganizationProfile>();
            Roles = new List<Role>();
            Rules = new List<Rule>();
            Preferences = new List<Preference>();
            Users = new List<User>();
            InitializePartial();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? OrganizationTypeId { get; set; }
        public bool? Cloaked { get; set; }
        public bool? Archive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedByUserId { get; set; }

        public virtual ICollection<AreaOfResponsibility> AreaOfResponsibilities { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Jurisdiction> Jurisdictions { get; set; }
        public virtual ICollection<OrganizationApplication> OrganizationApplications { get; set; }
        public virtual ICollection<OrganizationNote> OrganizationNotes { get; set; }
        public virtual ICollection<OrganizationProfile> OrganizationProfiles { get; set; }
        public virtual ICollection<Preference> Preferences { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Rule> Rules { get; set; }
        public virtual ICollection<User> Users { get; set; }

        public virtual OrganizationType OrganizationType { get; set; }

        partial void InitializePartial();
    }
}