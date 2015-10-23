using Spectrum.Core.Data.Models;
using System.Threading.Tasks;

namespace Spectrum.Core.Data.Repositories.Interfaces
{
    public interface IOrganizationProfileRepository : IEntityRepository<OrganizationProfile>
    {
        OrganizationProfile Find(int organizationProfileId);
        Task<OrganizationProfile> FindAsync(int organizationProfileId);
        void Save();
        Task SaveAsync();
    }
}