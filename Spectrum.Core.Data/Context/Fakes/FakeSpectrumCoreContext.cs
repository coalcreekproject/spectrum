using System;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using Spectrum.Core.Data.Context.Interfaces;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Context.Fakes
{
    public class FakeSpectrumCoreContext : ICoreDbContext
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
        public IDbSet<JurisdictionProfile> JusrisdictionProfiles { get; set; }
        public IDbSet<Message> Messages { get; set; }
        public IDbSet<Organization> Organizations { get; set; }
        public IDbSet<OrganizationApplication> OrganizationApplications { get; set; }
        public IDbSet<OrganizationNote> OrganizationNotes { get; set; }
        public IDbSet<OrganizationProfile> OrganizationProfiles { get; set; }
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
        public IDbSet<UserExternalLogin> UserLogins { get; set; }
        public IDbSet<UserNote> UserNotes { get; set; }
        public IDbSet<UserProfile> UserProfiles { get; set; }

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
            Messages = new FakeDbSet<Message>();
            Organizations = new FakeDbSet<Organization>();
            OrganizationApplications = new FakeDbSet<OrganizationApplication>();
            OrganizationNotes = new FakeDbSet<OrganizationNote>();
            OrganizationProfiles = new FakeDbSet<OrganizationProfile>();
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
            UserLogins = new FakeDbSet<UserExternalLogin>();
            UserNotes = new FakeDbSet<UserNote>();
            UserProfiles = new FakeDbSet<UserProfile>();
        }

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
