using System;
using System.Collections.Generic;

namespace Spectrum.Logic.Models
{
    [Serializable]
    public class OrganizationModel
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name
        public int? OrganizationTypeId { get; set; } // OrganizationTypeId

        // Reverse navigation
        public virtual ICollection<AddressNorthAmericaModel> AddressNorthAmericas { get; set; } // Many to many mapping
        public virtual ICollection<AreaOfResponsibilityModel> AreaOfResponsibilities { get; set; }
        // AreaOfResponsibility.FK_AreaOfResponsibility_OrganizationId
        public virtual ICollection<GroupModel> Groups { get; set; } // Group.FK_Group_Organization
        public virtual ICollection<JurisdictionModel> Jurisdictions { get; set; }
        // Jurisdiction.FK_Jurisdiction_Organization
        public virtual ICollection<OrganizationApplicationModel> OrganizationApplications { get; set; }
        // Many to many mapping
        public virtual ICollection<OrganizationProfileModel> OrganizationProfiles { get; set; }
        // OrganizationProfile.FK_OrganizationProfile_Organization
        public virtual ICollection<PreferenceModel> Preferences { get; set; } // Many to many mapping
        public virtual ICollection<RoleModel> Roles { get; set; } // Role.FK_Role_Organization
        public virtual ICollection<RuleModel> Rules { get; set; } // Rule.FK_Rule_Organization
        public virtual ICollection<UserModel> Users { get; set; } // Many to many mapping
        public virtual ICollection<UserProfileModel> UserProfiles { get; set; }
        // UserProfile.FK_UserProfile_OrganizationId

        // Foreign keys
        public virtual OrganizationTypeModel OrganizationType { get; set; } // FK_Organization_OrganizationType

        public OrganizationModel()
        {
            AreaOfResponsibilities = new List<AreaOfResponsibilityModel>();
            Groups = new List<GroupModel>();
            Jurisdictions = new List<JurisdictionModel>();
            OrganizationApplications = new List<OrganizationApplicationModel>();
            OrganizationProfiles = new List<OrganizationProfileModel>();
            Roles = new List<RoleModel>();
            Rules = new List<RuleModel>();
            UserProfiles = new List<UserProfileModel>();
            AddressNorthAmericas = new List<AddressNorthAmericaModel>();
            Preferences = new List<PreferenceModel>();
            Users = new List<UserModel>();
        }
    }
}
