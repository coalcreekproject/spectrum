using System;
using System.Collections.Generic;

namespace Spectrum.Core.Data.Models
{
    public partial class User
    {
        public User()
        {
            UserApplications = new List<UserApplication>();
            UserAreaOfResponsibilities = new List<UserAreaOfResponsibility>();
            UserClaims = new List<UserClaim>();
            UserExternalLogins = new List<UserExternalLogin>();
            UserNotes = new List<UserNote>();
            UserProfiles = new List<UserProfile>();
            Groups = new List<Group>();
            Jurisdictions = new List<Jurisdiction>();
            Organizations = new List<Organization>();
            Positions = new List<Position>();
            Preferences = new List<Preference>();
            Roles = new List<Role>();
            InitializePartial();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public bool? Cloaked { get; set; }
        public bool? Archive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedByUserId { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Jurisdiction> Jurisdictions { get; set; }
        public virtual ICollection<Organization> Organizations { get; set; }
        public virtual ICollection<Position> Positions { get; set; }
        public virtual ICollection<Preference> Preferences { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<UserApplication> UserApplications { get; set; }
        public virtual ICollection<UserAreaOfResponsibility> UserAreaOfResponsibilities { get; set; }
        public virtual ICollection<UserClaim> UserClaims { get; set; }
        public virtual ICollection<UserExternalLogin> UserExternalLogins { get; set; }
        public virtual ICollection<UserNote> UserNotes { get; set; }
        public virtual ICollection<UserProfile> UserProfiles { get; set; }

        partial void InitializePartial();
    }
}