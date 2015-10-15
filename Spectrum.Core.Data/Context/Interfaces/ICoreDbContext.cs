using System;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Context.Interfaces
{
    public interface ICoreDbContext : IDisposable
    {
        IDbSet<AddressInternational> AddressInternationals { get; set; } // AddressInternational
        IDbSet<AddressNorthAmerica> AddressNorthAmericas { get; set; } // AddressNorthAmerica
        IDbSet<Application> Applications { get; set; } // Application
        IDbSet<ApplicationProfile> ApplicationProfiles { get; set; } // ApplicationProfile
        IDbSet<AreaOfResponsibility> AreaOfResponsibilities { get; set; } // AreaOfResponsibility
        IDbSet<Group> Groups { get; set; } // Group
        IDbSet<Jurisdiction> Jurisdictions { get; set; } // Jurisdiction
        IDbSet<JusrisdictionProfile> JusrisdictionProfiles { get; set; } // JusrisdictionProfile
        IDbSet<Organization> Organizations { get; set; } // Organization
        IDbSet<OrganizationApplication> OrganizationApplications { get; set; } // OrganizationApplications
        IDbSet<OrganizationProfile> OrganizationProfiles { get; set; } // OrganizationProfile
        IDbSet<OrganizationType> OrganizationTypes { get; set; } // OrganizationType
        IDbSet<Parameter> Parameters { get; set; } // Parameter
        IDbSet<Position> Positions { get; set; } // Position
        IDbSet<Preference> Preferences { get; set; } // Preference
        IDbSet<Role> Roles { get; set; } // Role
        IDbSet<Rule> Rules { get; set; } // Rule
        IDbSet<RuleType> RuleTypes { get; set; } // RuleType
        IDbSet<User> Users { get; set; } // User
        IDbSet<UserApplication> UserApplications { get; set; } // UserApplication
        IDbSet<UserClaim> UserClaims { get; set; } // UserClaim
        IDbSet<UserLogin> UserExternalLogins { get; set; } // UserExternalLogin
        IDbSet<UserProfile> UserProfiles { get; set; } // UserProfile

        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }

}
