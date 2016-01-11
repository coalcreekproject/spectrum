using System;
using System.Collections.Generic;

namespace Spectrum.Logic.Models
{
    [Serializable]
    public class UserModel
    {
        public UserModel()
        {
            UserApplicationModels = new List<UserApplicationModel>();
            UserAreaOfResponsibilityModels = new List<UserAreaOfResponsibilityModel>();
            UserClaimModels = new List<UserClaimModel>();
            UserExternalLoginModels = new List<UserExternalLoginModel>();
            UserNoteModels = new List<UserNoteModel>();
            UserProfileModels = new List<UserProfileModel>();
            GroupModels = new List<GroupModel>();
            JurisdictionModels = new List<JurisdictionModel>();
            OrganizationModels = new List<OrganizationModel>();
            PositionModels = new List<PositionModel>();
            PreferenceModels = new List<PreferenceModel>();
            RoleModels = new List<RoleModel>();
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

        public virtual ICollection<GroupModel> GroupModels { get; set; }
        public virtual ICollection<JurisdictionModel> JurisdictionModels { get; set; }
        public virtual ICollection<OrganizationModel> OrganizationModels { get; set; }
        public virtual ICollection<PositionModel> PositionModels { get; set; }
        public virtual ICollection<PreferenceModel> PreferenceModels { get; set; }
        public virtual ICollection<RoleModel> RoleModels { get; set; }
        public virtual ICollection<UserApplicationModel> UserApplicationModels { get; set; }
        public virtual ICollection<UserAreaOfResponsibilityModel> UserAreaOfResponsibilityModels { get; set; }
        public virtual ICollection<UserClaimModel> UserClaimModels { get; set; }
        public virtual ICollection<UserExternalLoginModel> UserExternalLoginModels { get; set; }
        public virtual ICollection<UserNoteModel> UserNoteModels { get; set; }
        public virtual ICollection<UserProfileModel> UserProfileModels { get; set; }
    }
}