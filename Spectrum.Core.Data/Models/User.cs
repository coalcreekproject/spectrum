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
            UserGroups = new List<UserGroup>();
            UserJurisdictions = new List<UserJurisdiction>();
            UserNotes = new List<UserNote>();
            UserOrganizations = new List<UserOrganization>();
            UserPositions = new List<UserPosition>();
            UserPreferences = new List<UserPreference>();
            UserProfiles = new List<UserProfile>();
            UserRoles = new List<UserRole>();
            InitializePartial();
        }

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

        public virtual ICollection<UserApplication> UserApplications { get; set; }
        public virtual ICollection<UserAreaOfResponsibility> UserAreaOfResponsibilities { get; set; }
        public virtual ICollection<UserClaim> UserClaims { get; set; }
        public virtual ICollection<UserExternalLogin> UserExternalLogins { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
        public virtual ICollection<UserJurisdiction> UserJurisdictions { get; set; }
        public virtual ICollection<UserNote> UserNotes { get; set; }
        public virtual ICollection<UserOrganization> UserOrganizations { get; set; }
        public virtual ICollection<UserPosition> UserPositions { get; set; }
        public virtual ICollection<UserPreference> UserPreferences { get; set; }
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public int Id { get; set; }
        public string UserName { get; set; }

        partial void InitializePartial();
    }
}