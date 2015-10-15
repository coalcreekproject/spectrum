using System.Linq;
using System.Threading.Tasks;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Repositories.Interfaces
{
    public interface IOrganizationRepository : IEntityRepository<Organization>
    {
        IQueryable<Organization> Organizations { get; }
        Task CreateOrganization(Organization organization);
        Task UpdateAsync(Organization organization);
        Task DeleteAsync(Organization organization);
        Task<Organization> FindByIdAsync(int organizationId);
        Task<Organization> FindByNameAsync(string organizationName);
    }
}