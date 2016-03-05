using System;
using System.Collections.Generic;

namespace Spectrum.Logic.Models
{
    [Serializable]
    public class UserModel
    {
        public UserModel()
        {
            UserProfiles = new List<UserProfileModel>();
            UserRoles = new List<UserRoleModel>();
            UserOrganizations = new List<UserOrganizationModel>();
            UserPositions = new List<UserPositionModel>();
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

        //Application state specific properties
        public int SelectedOrganizationId { get; set; }
        public string SelectedOrganizationName { get; set; }
        public int SelectedRoleId { get; set; }
        public string SelectedRoleName { get; set; }
        public int SelectedPositionId { get; set; }
        public string SelectedPositionName { get; set; }

        public virtual ICollection<UserOrganizationModel> UserOrganizations { get; set; }
        public virtual ICollection<UserPositionModel> UserPositions { get; set; }
        public virtual ICollection<UserRoleModel> UserRoles { get; set; }
        public virtual ICollection<UserProfileModel> UserProfiles { get; set; }
    }
}