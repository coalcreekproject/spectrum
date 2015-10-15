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

        public int Id { get; set; } // Id (Primary key)
        public string UserName { get; set; } // UserName
        public string Email { get; set; } // Email
        public bool EmailConfirmed { get; set; } // EmailConfirmed
        public string PasswordHash { get; set; } // PasswordHash
        public string SecurityStamp { get; set; } // SecurityStamp
        public string PhoneNumber { get; set; } // PhoneNumber
        public bool PhoneNumberConfirmed { get; set; } // PhoneNumberConfirmed
        public bool TwoFactorEnabled { get; set; } // TwoFactorEnabled
        public DateTime? LockoutEndDateUtc { get; set; } // LockoutEndDateUtc
        public bool LockoutEnabled { get; set; } // LockoutEnabled
        public int AccessFailedCount { get; set; } // AccessFailedCount

        // Reverse navigation
        public virtual ICollection<AddressNorthAmericaModel> AddressNorthAmericas { get; set; } // Many to many mapping
        public virtual ICollection<AreaOfResponsibilityModel> AreaOfResponsibilities { get; set; }
        // Many to many mapping
        public virtual ICollection<GroupModel> Groups { get; set; } // Many to many mapping
        public virtual ICollection<JurisdictionModel> Jurisdictions { get; set; } // Many to many mapping
        public virtual ICollection<OrganizationModel> Organizations { get; set; } // Many to many mapping
        public virtual ICollection<PositionModel> Positions { get; set; } // Many to many mapping
        public virtual ICollection<PreferenceModel> Preferences { get; set; } // Many to many mapping
        public virtual ICollection<RoleModel> Roles { get; set; } // Many to many mapping
        public virtual ICollection<UserApplicationModel> UserApplications { get; set; } // Many to many mapping
        public virtual ICollection<UserClaimModel> UserClaims { get; set; } // UserClaim.FK_UserClaim_User
        public virtual ICollection<UserLoginModel> UserExternalLogins { get; set; } // Many to many mapping
        public virtual ICollection<UserProfileModel> UserProfiles { get; set; } // UserProfile.FK_UserProfile_User
    }
}