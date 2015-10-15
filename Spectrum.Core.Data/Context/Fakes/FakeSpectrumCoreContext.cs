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
        public IDbSet<AddressInternational> AddressInternationals { get; set; }
        public IDbSet<AddressNorthAmerica> AddressNorthAmericas { get; set; }
        public IDbSet<Application> Applications { get; set; }
        public IDbSet<ApplicationProfile> ApplicationProfiles { get; set; }
        public IDbSet<AreaOfResponsibility> AreaOfResponsibilities { get; set; }
        public IDbSet<Group> Groups { get; set; }
        public IDbSet<Jurisdiction> Jurisdictions { get; set; }
        public IDbSet<JusrisdictionProfile> JusrisdictionProfiles { get; set; }
        public IDbSet<Organization> Organizations { get; set; }
        public IDbSet<OrganizationApplication> OrganizationApplications { get; set; }
        public IDbSet<OrganizationProfile> OrganizationProfiles { get; set; }
        public IDbSet<OrganizationType> OrganizationTypes { get; set; }
        public IDbSet<Parameter> Parameters { get; set; }
        public IDbSet<Position> Positions {get; set;}
        public IDbSet<Preference> Preferences { get; set; }
        public IDbSet<Role> Roles { get; set; }
        public IDbSet<Rule> Rules { get; set; }
        public IDbSet<RuleType> RuleTypes { get; set; }
        public IDbSet<User> Users { get; set; }
        public IDbSet<UserApplication> UserApplications { get; set; }
        public IDbSet<UserClaim> UserClaims { get; set; }
        public IDbSet<UserLogin> UserExternalLogins { get; set; }
        public IDbSet<UserProfile> UserProfiles { get; set; }

        public FakeSpectrumCoreContext()
        {
            AddressInternationals = new FakeDbSet<AddressInternational>();
            AddressNorthAmericas = new FakeDbSet<AddressNorthAmerica>();
            Applications = new FakeDbSet<Application>();
            ApplicationProfiles = new FakeDbSet<ApplicationProfile>();
            AreaOfResponsibilities = new FakeDbSet<AreaOfResponsibility>();
            Groups = new FakeDbSet<Group>();
            Jurisdictions = new FakeDbSet<Jurisdiction>();
            JusrisdictionProfiles = new FakeDbSet<JusrisdictionProfile>();
            Organizations = new FakeDbSet<Organization>();
            OrganizationApplications = new FakeDbSet<OrganizationApplication>();
            OrganizationProfiles = new FakeDbSet<OrganizationProfile>();
            OrganizationTypes = new FakeDbSet<OrganizationType>();
            Parameters = new FakeDbSet<Parameter>();
            Preferences = new FakeDbSet<Preference>();
            Roles = new FakeDbSet<Role>();
            Rules = new FakeDbSet<Rule>();
            RuleTypes = new FakeDbSet<RuleType>();
            Users = new FakeDbSet<User>();
            UserApplications = new FakeDbSet<UserApplication>();
            UserClaims = new FakeDbSet<UserClaim>();
            UserExternalLogins = new FakeDbSet<UserLogin>();
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
