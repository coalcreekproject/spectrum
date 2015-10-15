using System.Data.Entity;
using Spectrum.Core.Data.Configuration;
using Spectrum.Core.Data.Context.Interfaces;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Context
{
    public partial class CoreDbContext : DbContext, ICoreDbContext
    {
        public IDbSet<AddressInternational> AddressInternationals { get; set; } // AddressInternational
        public IDbSet<AddressNorthAmerica> AddressNorthAmericas { get; set; } // AddressNorthAmerica
        public IDbSet<Application> Applications { get; set; } // Application
        public IDbSet<ApplicationProfile> ApplicationProfiles { get; set; } // ApplicationProfile
        public IDbSet<AreaOfResponsibility> AreaOfResponsibilities { get; set; } // AreaOfResponsibility
        public IDbSet<Group> Groups { get; set; } // Group
        public IDbSet<Jurisdiction> Jurisdictions { get; set; } // Jurisdiction
        public IDbSet<JusrisdictionProfile> JusrisdictionProfiles { get; set; } // JusrisdictionProfile
        public IDbSet<Organization> Organizations { get; set; } // Organization
        public IDbSet<OrganizationApplication> OrganizationApplications { get; set; } // OrganizationApplications
        public IDbSet<OrganizationProfile> OrganizationProfiles { get; set; } // OrganizationProfile
        public IDbSet<OrganizationType> OrganizationTypes { get; set; } // OrganizationType
        public IDbSet<Parameter> Parameters { get; set; } // Parameter
        public IDbSet<Position> Positions { get; set; } // Position
        public IDbSet<Preference> Preferences { get; set; } // Preference
        public IDbSet<Role> Roles { get; set; } // Role
        public IDbSet<Rule> Rules { get; set; } // Rule
        public IDbSet<RuleType> RuleTypes { get; set; } // RuleType
        public IDbSet<User> Users { get; set; } // User
        public IDbSet<UserApplication> UserApplications { get; set; } // UserApplication
        public IDbSet<UserClaim> UserClaims { get; set; } // UserClaim
        public IDbSet<UserLogin> UserExternalLogins { get; set; } // UserExternalLogin
        public IDbSet<UserProfile> UserProfiles { get; set; } // UserProfile

        public static CoreDbContext Create()
        {
            return new CoreDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new AddressInternationalConfiguration());
            modelBuilder.Configurations.Add(new AddressNorthAmericaConfiguration());
            modelBuilder.Configurations.Add(new ApplicationConfiguration());
            modelBuilder.Configurations.Add(new ApplicationProfileConfiguration());
            modelBuilder.Configurations.Add(new AreaOfResponsibilityConfiguration());
            modelBuilder.Configurations.Add(new GroupConfiguration());
            modelBuilder.Configurations.Add(new JurisdictionConfiguration());
            modelBuilder.Configurations.Add(new JusrisdictionProfileConfiguration());
            modelBuilder.Configurations.Add(new OrganizationConfiguration());
            modelBuilder.Configurations.Add(new OrganizationApplicationConfiguration());
            modelBuilder.Configurations.Add(new OrganizationProfileConfiguration());
            modelBuilder.Configurations.Add(new OrganizationTypeConfiguration());
            modelBuilder.Configurations.Add(new ParameterConfiguration());
            modelBuilder.Configurations.Add(new PositionConfiguration());
            modelBuilder.Configurations.Add(new PreferenceConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new RuleConfiguration());
            modelBuilder.Configurations.Add(new RuleTypeConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new UserApplicationConfiguration());
            modelBuilder.Configurations.Add(new UserClaimConfiguration());
            modelBuilder.Configurations.Add(new UserExternalLoginConfiguration());
            modelBuilder.Configurations.Add(new UserProfileConfiguration());

            OnModelCreatingPartial(modelBuilder);
        }

        public static DbModelBuilder CreateModel(DbModelBuilder modelBuilder, string schema)
        {
            modelBuilder.Configurations.Add(new AddressInternationalConfiguration(schema));
            modelBuilder.Configurations.Add(new AddressNorthAmericaConfiguration(schema));
            modelBuilder.Configurations.Add(new ApplicationConfiguration(schema));
            modelBuilder.Configurations.Add(new ApplicationProfileConfiguration(schema));
            modelBuilder.Configurations.Add(new AreaOfResponsibilityConfiguration(schema));
            modelBuilder.Configurations.Add(new GroupConfiguration(schema));
            modelBuilder.Configurations.Add(new JurisdictionConfiguration(schema));
            modelBuilder.Configurations.Add(new JusrisdictionProfileConfiguration(schema));
            modelBuilder.Configurations.Add(new OrganizationConfiguration(schema));
            modelBuilder.Configurations.Add(new OrganizationApplicationConfiguration(schema));
            modelBuilder.Configurations.Add(new OrganizationProfileConfiguration(schema));
            modelBuilder.Configurations.Add(new OrganizationTypeConfiguration(schema));
            modelBuilder.Configurations.Add(new ParameterConfiguration(schema));
            modelBuilder.Configurations.Add(new PositionConfiguration(schema)); 
            modelBuilder.Configurations.Add(new PreferenceConfiguration(schema));
            modelBuilder.Configurations.Add(new RoleConfiguration(schema));
            modelBuilder.Configurations.Add(new RuleConfiguration(schema));
            modelBuilder.Configurations.Add(new RuleTypeConfiguration(schema));
            modelBuilder.Configurations.Add(new UserConfiguration(schema));
            modelBuilder.Configurations.Add(new UserApplicationConfiguration(schema));
            modelBuilder.Configurations.Add(new UserClaimConfiguration(schema));
            modelBuilder.Configurations.Add(new UserExternalLoginConfiguration(schema));
            modelBuilder.Configurations.Add(new UserProfileConfiguration(schema));
            return modelBuilder;
        }

        partial void InitializePartial();
        partial void OnModelCreatingPartial(DbModelBuilder modelBuilder);
    }
}
