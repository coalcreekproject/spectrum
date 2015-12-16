using System;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using Spectrum.Data.Core.Models;

namespace Spectrum.Data.Core.Context.Interfaces
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
        IDbSet<JurisdictionProfile> JurisdictionProfiles { get; set; }
        IDbSet<Message> Messages { get; set; }
        IDbSet<Organization> Organizations { get; set; }
        IDbSet<OrganizationApplication> OrganizationApplications { get; set; }
        IDbSet<OrganizationLicense> OrganizationLicenses { get; set; }
        IDbSet<OrganizationNote> OrganizationNotes { get; set; }
        IDbSet<OrganizationPreference> OrganizationPreferences { get; set; }
        IDbSet<OrganizationProfile> OrganizationProfiles { get; set; }
        IDbSet<OrganizationProfileAddress> OrganizationProfileAddresses { get; set; }
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
        IDbSet<UserExternalLogin> UserExternalLogins { get; set; }
        IDbSet<UserGroup> UserGroups { get; set; }
        IDbSet<UserJurisdiction> UserJurisdictions { get; set; }
        IDbSet<UserLicense> UserLicenses { get; set; }
        IDbSet<UserNote> UserNotes { get; set; }
        IDbSet<UserOrganization> UserOrganizations { get; set; }
        IDbSet<UserPosition> UserPositions { get; set; }
        IDbSet<UserPreference> UserPreferences { get; set; }
        IDbSet<UserProfile> UserProfiles { get; set; }
        IDbSet<UserProfileAddress> UserProfileAddresses { get; set; }
        IDbSet<UserRole> UserRoles { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}