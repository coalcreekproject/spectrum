using System;
using System.Collections.Generic;

namespace Spectrum.Core.Data.Models
{
    // User

    public partial class User
    {
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
        public virtual ICollection<AddressNorthAmerica> AddressNorthAmericas { get; set; } // Many to many mapping
        public virtual ICollection<AreaOfResponsibility> AreaOfResponsibilities { get; set; } // Many to many mapping
        public virtual ICollection<Group> Groups { get; set; } // Many to many mapping
        public virtual ICollection<Jurisdiction> Jurisdictions { get; set; } // Many to many mapping
        public virtual ICollection<Organization> Organizations { get; set; } // Many to many mapping
        public virtual ICollection<Position> Positions { get; set; } // Many to many mapping
        public virtual ICollection<Preference> Preferences { get; set; } // Many to many mapping
        public virtual ICollection<Role> Roles { get; set; } // Many to many mapping
        public virtual ICollection<UserApplication> UserApplications { get; set; } // Many to many mapping
        public virtual ICollection<UserClaim> UserClaims { get; set; } // UserClaim.FK_UserClaim_User
        public virtual ICollection<UserLogin> UserExternalLogins { get; set; } // Many to many mapping
        public virtual ICollection<UserProfile> UserProfiles { get; set; } // UserProfile.FK_UserProfile_User
        
        public User()
        {
            UserApplications = new List<UserApplication>();
            UserClaims = new List<UserClaim>();
            UserExternalLogins = new List<UserLogin>();
            UserProfiles = new List<UserProfile>();
            AddressNorthAmericas = new List<AddressNorthAmerica>();
            AreaOfResponsibilities = new List<AreaOfResponsibility>();
            Groups = new List<Group>();
            Jurisdictions = new List<Jurisdiction>();
            Organizations = new List<Organization>();
            Positions = new List<Position>();
            Preferences = new List<Preference>();
            Roles = new List<Role>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
