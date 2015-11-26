using System;
using System.Collections.Generic;

namespace Spectrum.Logic.Models
{
    [Serializable]
    public class OrganizationModel
    {
        public OrganizationModel()
        {
            AreaOfResponsibilitiyModels = new List<AreaOfResponsibilityModel>();
            GroupModels = new List<GroupModel>();
            JurisdictionModels = new List<JurisdictionModel>();
            OrganizationApplicationModels = new List<OrganizationApplicationModel>();
            OrganizationNoteModels = new List<OrganizationNoteModel>();
            OrganizationProfileModels = new List<OrganizationProfileModel>();
            RoleModels = new List<RoleModel>();
            RuleModels = new List<RuleModel>();
            PreferenceModels = new List<PreferenceModel>();
            UserModels = new List<UserModel>();
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

        public virtual ICollection<AreaOfResponsibilityModel> AreaOfResponsibilitiyModels { get; set; }
        public virtual ICollection<GroupModel> GroupModels { get; set; }
        public virtual ICollection<JurisdictionModel> JurisdictionModels { get; set; }
        public virtual ICollection<OrganizationApplicationModel> OrganizationApplicationModels { get; set; }
        public virtual ICollection<OrganizationNoteModel> OrganizationNoteModels { get; set; }
        public virtual ICollection<OrganizationProfileModel> OrganizationProfileModels { get; set; }
        public virtual ICollection<PreferenceModel> PreferenceModels { get; set; }
        public virtual ICollection<RoleModel> RoleModels { get; set; }
        public virtual ICollection<RuleModel> RuleModels { get; set; }
        public virtual ICollection<UserModel> UserModels { get; set; }

        public virtual OrganizationTypeModel OrganizationTypeModel { get; set; }
    }
}