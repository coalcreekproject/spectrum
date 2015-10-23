using System.Linq;
using System.Threading.Tasks;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Repositories.Interfaces
{
    public interface IOrganizationRepository : IEntityRepository<Organization>
    {
        Organization Find(int organizationId);
        Task<Organization> FindAsync(int organizationId);
        void Save();
        Task SaveAsync();

    }
}