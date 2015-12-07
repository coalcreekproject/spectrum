using System.Data.Entity;
using Spectrum.Core.Data.Configuration;
using Spectrum.Core.Data.Context.Interfaces;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Context
{
    public partial class CoreDbContext : DbContext, ICoreDbContext
    {
        public IDbSet<Address> Addresses { get; set; }
        public IDbSet<Application> Applications { get; set; }
        public IDbSet<ApplicationNote> ApplicationNotes { get; set; }
        public IDbSet<ApplicationParameter> ApplicationParameters { get; set; }
        public IDbSet<ApplicationProfile> ApplicationProfiles { get; set; }
        public IDbSet<AreaOfResponsibility> AreaOfResponsibilities { get; set; }
        public IDbSet<AreaOfResponsibilityNote> AreaOfResponsibilityNotes { get; set; }
        public IDbSet<AreaOfResponsibilityProfile> AreaOfResponsibilityProfiles { get; set; }
        public IDbSet<Contact> Contacts { get; set; }
        public IDbSet<Group> Groups { get; set; }
        public IDbSet<Jurisdiction> Jurisdictions { get; set; }
        public IDbSet<JurisdictionNote> JurisdictionNotes { get; set; }
        public IDbSet<JurisdictionProfile> JurisdictionProfiles { get; set; }
        public IDbSet<Message> Messages { get; set; }
        public IDbSet<Organization> Organizations { get; set; }
        public IDbSet<OrganizationApplication> OrganizationApplications { get; set; }
        public IDbSet<OrganizationLicense> OrganizationLicenses { get; set; }
        public IDbSet<OrganizationNote> OrganizationNotes { get; set; }
        public IDbSet<OrganizationPreference> OrganizationPreferences { get; set; }
        public IDbSet<OrganizationProfile> OrganizationProfiles { get; set; }
        public IDbSet<OrganizationProfileAddress> OrganizationProfileAddresses { get; set; }
        public IDbSet<OrganizationType> OrganizationTypes { get; set; }
        public IDbSet<Parameter> Parameters { get; set; }
        public IDbSet<Position> Positions { get; set; }
        public IDbSet<Preference> Preferences { get; set; }
        public IDbSet<Role> Roles { get; set; }
        public IDbSet<Rule> Rules { get; set; }
        public IDbSet<RuleParameter> RuleParameters { get; set; }
        public IDbSet<RuleType> RuleTypes { get; set; }
        public IDbSet<User> Users { get; set; }
        public IDbSet<UserApplication> UserApplications { get; set; }
        public IDbSet<UserAreaOfResponsibility> UserAreaOfResponsibilities { get; set; }
        public IDbSet<UserClaim> UserClaims { get; set; }
        public IDbSet<UserExternalLogin> UserExternalLogins { get; set; }
        public IDbSet<UserGroup> UserGroups { get; set; }
        public IDbSet<UserJurisdiction> UserJurisdictions { get; set; }
        public IDbSet<UserLicense> UserLicenses { get; set; }
        public IDbSet<UserNote> UserNotes { get; set; }
        public IDbSet<UserOrganization> UserOrganizations { get; set; }
        public IDbSet<UserPosition> UserPositions { get; set; }
        public IDbSet<UserPreference> UserPreferences { get; set; }
        public IDbSet<UserProfile> UserProfiles { get; set; }
        public IDbSet<UserProfileAddress> UserProfileAddresses { get; set; }
        public IDbSet<UserRole> UserRoles { get; set; }

        public static CoreDbContext Create()
        {
            return new CoreDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new AddressConfiguration());
            modelBuilder.Configurations.Add(new ApplicationConfiguration());
            modelBuilder.Configurations.Add(new ApplicationNoteConfiguration());
            modelBuilder.Configurations.Add(new ApplicationParameterConfiguration());
            modelBuilder.Configurations.Add(new ApplicationProfileConfiguration());
            modelBuilder.Configurations.Add(new AreaOfResponsibilityConfiguration());
            modelBuilder.Configurations.Add(new AreaOfResponsibilityNoteConfiguration());
            modelBuilder.Configurations.Add(new AreaOfResponsibilityProfileConfiguration());
            modelBuilder.Configurations.Add(new ContactConfiguration());
            modelBuilder.Configurations.Add(new GroupConfiguration());
            modelBuilder.Configurations.Add(new JurisdictionConfiguration());
            modelBuilder.Configurations.Add(new JurisdictionNoteConfiguration());
            modelBuilder.Configurations.Add(new JurisdictionProfileConfiguration());
            modelBuilder.Configurations.Add(new MessageConfiguration());
            modelBuilder.Configurations.Add(new OrganizationConfiguration());
            modelBuilder.Configurations.Add(new OrganizationApplicationConfiguration());
            modelBuilder.Configurations.Add(new OrganizationLicenseConfiguration());
            modelBuilder.Configurations.Add(new OrganizationNoteConfiguration());
            modelBuilder.Configurations.Add(new OrganizationPreferenceConfiguration());
            modelBuilder.Configurations.Add(new OrganizationProfileConfiguration());
            modelBuilder.Configurations.Add(new OrganizationProfileAddressConfiguration());
            modelBuilder.Configurations.Add(new OrganizationTypeConfiguration());
            modelBuilder.Configurations.Add(new ParameterConfiguration());
            modelBuilder.Configurations.Add(new PositionConfiguration());
            modelBuilder.Configurations.Add(new PreferenceConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new RuleConfiguration());
            modelBuilder.Configurations.Add(new RuleParameterConfiguration());
            modelBuilder.Configurations.Add(new RuleTypeConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new UserApplicationConfiguration());
            modelBuilder.Configurations.Add(new UserAreaOfResponsibilityConfiguration());
            modelBuilder.Configurations.Add(new UserClaimConfiguration());
            modelBuilder.Configurations.Add(new UserExternalLoginConfiguration());
            modelBuilder.Configurations.Add(new UserGroupConfiguration());
            modelBuilder.Configurations.Add(new UserJurisdictionConfiguration());
            modelBuilder.Configurations.Add(new UserLicenseConfiguration());
            modelBuilder.Configurations.Add(new UserNoteConfiguration());
            modelBuilder.Configurations.Add(new UserOrganizationConfiguration());
            modelBuilder.Configurations.Add(new UserPositionConfiguration());
            modelBuilder.Configurations.Add(new UserPreferenceConfiguration());
            modelBuilder.Configurations.Add(new UserProfileConfiguration());
            modelBuilder.Configurations.Add(new UserProfileAddressConfiguration());
            modelBuilder.Configurations.Add(new UserRoleConfiguration());

            OnModelCreatingPartial(modelBuilder);
        }

        public static DbModelBuilder CreateModel(DbModelBuilder modelBuilder, string schema)
        {
            modelBuilder.Configurations.Add(new AddressConfiguration(schema));
            modelBuilder.Configurations.Add(new ApplicationConfiguration(schema));
            modelBuilder.Configurations.Add(new ApplicationNoteConfiguration(schema));
            modelBuilder.Configurations.Add(new ApplicationParameterConfiguration(schema));
            modelBuilder.Configurations.Add(new ApplicationProfileConfiguration(schema));
            modelBuilder.Configurations.Add(new AreaOfResponsibilityConfiguration(schema));
            modelBuilder.Configurations.Add(new AreaOfResponsibilityNoteConfiguration(schema));
            modelBuilder.Configurations.Add(new AreaOfResponsibilityProfileConfiguration(schema));
            modelBuilder.Configurations.Add(new ContactConfiguration(schema));
            modelBuilder.Configurations.Add(new GroupConfiguration(schema));
            modelBuilder.Configurations.Add(new JurisdictionConfiguration(schema));
            modelBuilder.Configurations.Add(new JurisdictionNoteConfiguration(schema));
            modelBuilder.Configurations.Add(new JurisdictionProfileConfiguration(schema));
            modelBuilder.Configurations.Add(new MessageConfiguration(schema));
            modelBuilder.Configurations.Add(new OrganizationConfiguration(schema));
            modelBuilder.Configurations.Add(new OrganizationApplicationConfiguration(schema));
            modelBuilder.Configurations.Add(new OrganizationLicenseConfiguration(schema));
            modelBuilder.Configurations.Add(new OrganizationNoteConfiguration(schema));
            modelBuilder.Configurations.Add(new OrganizationPreferenceConfiguration(schema));
            modelBuilder.Configurations.Add(new OrganizationProfileConfiguration(schema));
            modelBuilder.Configurations.Add(new OrganizationProfileAddressConfiguration(schema));
            modelBuilder.Configurations.Add(new OrganizationTypeConfiguration(schema));
            modelBuilder.Configurations.Add(new ParameterConfiguration(schema));
            modelBuilder.Configurations.Add(new PositionConfiguration(schema));
            modelBuilder.Configurations.Add(new PreferenceConfiguration(schema));
            modelBuilder.Configurations.Add(new RoleConfiguration(schema));
            modelBuilder.Configurations.Add(new RuleConfiguration(schema));
            modelBuilder.Configurations.Add(new RuleParameterConfiguration(schema));
            modelBuilder.Configurations.Add(new RuleTypeConfiguration(schema));
            modelBuilder.Configurations.Add(new UserConfiguration(schema));
            modelBuilder.Configurations.Add(new UserApplicationConfiguration(schema));
            modelBuilder.Configurations.Add(new UserAreaOfResponsibilityConfiguration(schema));
            modelBuilder.Configurations.Add(new UserClaimConfiguration(schema));
            modelBuilder.Configurations.Add(new UserExternalLoginConfiguration(schema));
            modelBuilder.Configurations.Add(new UserGroupConfiguration(schema));
            modelBuilder.Configurations.Add(new UserJurisdictionConfiguration(schema));
            modelBuilder.Configurations.Add(new UserLicenseConfiguration(schema));
            modelBuilder.Configurations.Add(new UserNoteConfiguration(schema));
            modelBuilder.Configurations.Add(new UserOrganizationConfiguration(schema));
            modelBuilder.Configurations.Add(new UserPositionConfiguration(schema));
            modelBuilder.Configurations.Add(new UserPreferenceConfiguration(schema));
            modelBuilder.Configurations.Add(new UserProfileConfiguration(schema));
            modelBuilder.Configurations.Add(new UserProfileAddressConfiguration(schema));
            modelBuilder.Configurations.Add(new UserRoleConfiguration(schema));
            return modelBuilder;
        }

        partial void InitializePartial();
        partial void OnModelCreatingPartial(DbModelBuilder modelBuilder);
    }
}