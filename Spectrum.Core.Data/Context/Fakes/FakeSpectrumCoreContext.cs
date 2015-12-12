using System;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using Spectrum.Data.Core.Context.Interfaces;
using Spectrum.Data.Core.Models;

namespace Spectrum.Data.Core.Context.Fakes
{
    public class FakeSpectrumCoreContext : ICoreDbContext
    {
        public FakeSpectrumCoreContext()
        {
            Addresses = new FakeDbSet<Address>();
            Applications = new FakeDbSet<Application>();
            ApplicationNotes = new FakeDbSet<ApplicationNote>();
            ApplicationParameters = new FakeDbSet<ApplicationParameter>();
            ApplicationProfiles = new FakeDbSet<ApplicationProfile>();
            AreaOfResponsibilities = new FakeDbSet<AreaOfResponsibility>();
            AreaOfResponsibilityNotes = new FakeDbSet<AreaOfResponsibilityNote>();
            AreaOfResponsibilityProfiles = new FakeDbSet<AreaOfResponsibilityProfile>();
            Contacts = new FakeDbSet<Contact>();
            Groups = new FakeDbSet<Group>();
            Jurisdictions = new FakeDbSet<Jurisdiction>();
            JurisdictionNotes = new FakeDbSet<JurisdictionNote>();
            JurisdictionProfiles = new FakeDbSet<JurisdictionProfile>();
            Messages = new FakeDbSet<Message>();
            Organizations = new FakeDbSet<Organization>();
            OrganizationApplications = new FakeDbSet<OrganizationApplication>();
            OrganizationLicenses = new FakeDbSet<OrganizationLicense>();
            OrganizationNotes = new FakeDbSet<OrganizationNote>();
            OrganizationPreferences = new FakeDbSet<OrganizationPreference>();
            OrganizationProfiles = new FakeDbSet<OrganizationProfile>();
            OrganizationProfileAddresses = new FakeDbSet<OrganizationProfileAddress>();
            OrganizationTypes = new FakeDbSet<OrganizationType>();
            Parameters = new FakeDbSet<Parameter>();
            Positions = new FakeDbSet<Position>();
            Preferences = new FakeDbSet<Preference>();
            Roles = new FakeDbSet<Role>();
            Rules = new FakeDbSet<Rule>();
            RuleParameters = new FakeDbSet<RuleParameter>();
            RuleTypes = new FakeDbSet<RuleType>();
            Users = new FakeDbSet<User>();
            UserApplications = new FakeDbSet<UserApplication>();
            UserAreaOfResponsibilities = new FakeDbSet<UserAreaOfResponsibility>();
            UserClaims = new FakeDbSet<UserClaim>();
            UserExternalLogins = new FakeDbSet<UserExternalLogin>();
            UserGroups = new FakeDbSet<UserGroup>();
            UserJurisdictions = new FakeDbSet<UserJurisdiction>();
            UserLicenses = new FakeDbSet<UserLicense>();
            UserNotes = new FakeDbSet<UserNote>();
            UserOrganizations = new FakeDbSet<UserOrganization>();
            UserPositions = new FakeDbSet<UserPosition>();
            UserPreferences = new FakeDbSet<UserPreference>();
            UserProfiles = new FakeDbSet<UserProfile>();
            UserProfileAddresses = new FakeDbSet<UserProfileAddress>();
            UserRoles = new FakeDbSet<UserRole>();
        }

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

        public int SaveChanges()
        {
            return 0;
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}