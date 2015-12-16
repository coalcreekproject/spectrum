using System.Threading.Tasks;
using Spectrum.Data.Core.Models;

namespace Spectrum.Data.Core.Repositories.Interfaces
{
    public interface IOrganizationProfileRepository : IEntityRepository<OrganizationProfile>
    {
        OrganizationProfile Find(int organizationProfileId);
        Task<OrganizationProfile> FindAsync(int organizationProfileId);
        void Save();
        Task SaveAsync();
    }
}