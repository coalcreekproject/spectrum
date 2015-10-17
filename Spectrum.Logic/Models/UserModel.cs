using System;
using System.Collections.Generic;

namespace Spectrum.Logic.Models
{
    // User

    public class UserModel
    {
        public UserModel()
        {
            UserApplications = new List<UserApplicationModel>();
            UserClaims = new List<UserClaimModel>();
            UserExternalLogins = new List<UserLoginModel>();
            UserProfiles = new List<UserProfileModel>();
            AddressNorthAmericas = new List<AddressNorthAmericaModel>();
            AreaOfResponsibilities = new List<AreaOfResponsibilityModel>();
            Groups = new List<GroupModel>();
            Jurisdictions = new List<JurisdictionModel>();
            Organizations = new List<OrganizationModel>();
            Positions = new List<PositionModel>();
            Preferences = new List<PreferenceModel>();
            Roles = new List<RoleModel>();
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

        //Used for logged in/cached user
        public DateTime LoginTime { get; set; }
        public DateTime LastActivity { get; set; }
        public int SessionDuration { get; set; }

        // Reverse navigation
        public virtual ICollection<AddressNorthAmericaModel> AddressNorthAmericas { get; set; }
        public virtual ICollection<AreaOfResponsibilityModel> AreaOfResponsibilities { get; set; }
        public virtual ICollection<GroupModel> Groups { get; set; }
        public virtual ICollection<JurisdictionModel> Jurisdictions { get; set; }
        public virtual ICollection<OrganizationModel> Organizations { get; set; }
        public virtual ICollection<PositionModel> Positions { get; set; }
        public virtual ICollection<PreferenceModel> Preferences { get; set; }
        public virtual ICollection<RoleModel> Roles { get; set; }
        public virtual ICollection<UserApplicationModel> UserApplications { get; set; }
        public virtual ICollection<UserClaimModel> UserClaims { get; set; }
        public virtual ICollection<UserLoginModel> UserExternalLogins { get; set; }
        public virtual ICollection<UserProfileModel> UserProfiles { get; set; }
    }
}