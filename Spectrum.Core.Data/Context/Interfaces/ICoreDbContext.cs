using System;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Context.Interfaces
{
    public interface ICoreDbContext : IDisposable
    {
        IDbSet<Address> Addresses { get; set; }
        IDbSet<Application> Applications { get; set; }
        IDbSet<ApplicationNote> ApplicationNotes { get; set; }
        IDbSet<ApplicationParameter> ApplicationParameters { get; set; }
        IDbSet<ApplicationProfile> ApplicationProfiles { get; set; }
        IDbSet<AreaOfResponsibility> AreaOfResponsibilities { get; set; }
        IDbSet<AreaOfResponsibilityNote> AreaOfResponsibilityNotes { get; set; }
        IDbSet<AreaOfResponsibilityProfile> AreaOfResponsibilityProfiles { get; set; }
        IDbSet<Contact> Contacts { get; set; }
        IDbSet<Group> Groups { get; set; }
        IDbSet<Jurisdiction> Jurisdictions { get; set; }
        IDbSet<JurisdictionNote> JurisdictionNotes { get; set; }
        IDbSet<JurisdictionProfile> JusrisdictionProfiles { get; set; }
        IDbSet<Message> Messages { get; set; }
        IDbSet<Organization> Organizations { get; set; }
        IDbSet<OrganizationApplication> OrganizationApplications { get; set; }
        IDbSet<OrganizationNote> OrganizationNotes { get; set; }
        IDbSet<OrganizationProfile> OrganizationProfiles { get; set; }
        IDbSet<OrganizationType> OrganizationTypes { get; set; }
        IDbSet<Parameter> Parameters { get; set; }
        IDbSet<Position> Positions { get; set; }
        IDbSet<Preference> Preferences { get; set; }
        IDbSet<Role> Roles { get; set; }
        IDbSet<Rule> Rules { get; set; }
        IDbSet<RuleParameter> RuleParameters { get; set; }
        IDbSet<RuleType> RuleTypes { get; set; }
        IDbSet<User> Users { get; set; }
        IDbSet<UserApplication> UserApplications { get; set; }
        IDbSet<UserAreaOfResponsibility> UserAreaOfResponsibilities { get; set; }
        IDbSet<UserClaim> UserClaims { get; set; }
        IDbSet<UserExternalLogin> UserLogins { get; set; }
        IDbSet<UserNote> UserNotes { get; set; }
        IDbSet<UserProfile> UserProfiles { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }

}
