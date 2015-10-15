using System.Collections.Generic;

namespace Spectrum.Core.Data.Models
{
    // Organization

    public partial class Organization
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name
        public int? OrganizationTypeId { get; set; } // OrganizationTypeId

        // Reverse navigation
        public virtual ICollection<AddressNorthAmerica> AddressNorthAmericas { get; set; } // Many to many mapping
        public virtual ICollection<AreaOfResponsibility> AreaOfResponsibilities { get; set; } // AreaOfResponsibility.FK_AreaOfResponsibility_OrganizationId
        public virtual ICollection<Group> Groups { get; set; } // Group.FK_Group_Organization
        public virtual ICollection<Jurisdiction> Jurisdictions { get; set; } // Jurisdiction.FK_Jurisdiction_Organization
        public virtual ICollection<OrganizationApplication> OrganizationApplications { get; set; } // Many to many mapping
        public virtual ICollection<OrganizationProfile> OrganizationProfiles { get; set; } // OrganizationProfile.FK_OrganizationProfile_Organization
        public virtual ICollection<Preference> Preferences { get; set; } // Many to many mapping
        public virtual ICollection<Role> Roles { get; set; } // Role.FK_Role_Organization
        public virtual ICollection<Rule> Rules { get; set; } // Rule.FK_Rule_Organization
        public virtual ICollection<User> Users { get; set; } // Many to many mapping
        public virtual ICollection<UserProfile> UserProfiles { get; set; } // UserProfile.FK_UserProfile_OrganizationId

        // Foreign keys
        public virtual OrganizationType OrganizationType { get; set; } // FK_Organization_OrganizationType

        public Organization()
        {
            AreaOfResponsibilities = new List<AreaOfResponsibility>();
            Groups = new List<Group>();
            Jurisdictions = new List<Jurisdiction>();
            OrganizationApplications = new List<OrganizationApplication>();
            OrganizationProfiles = new List<OrganizationProfile>();
            Roles = new List<Role>();
            Rules = new List<Rule>();
            UserProfiles = new List<UserProfile>();
            AddressNorthAmericas = new List<AddressNorthAmerica>();
            Preferences = new List<Preference>();
            Users = new List<User>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
