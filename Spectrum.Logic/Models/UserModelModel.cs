using System;
using System.Collections.Generic;

namespace Spectrum.Logic.Models
{
    public partial class UserModel
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

        partial void InitializePartial();
    }
}