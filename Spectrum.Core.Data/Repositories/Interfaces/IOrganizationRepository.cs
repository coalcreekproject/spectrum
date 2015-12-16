using System.Threading.Tasks;
using Spectrum.Data.Core.Models;

namespace Spectrum.Data.Core.Repositories.Interfaces
{
    public interface IOrganizationRepository : IEntityRepository<Organization>
    {
        Organization Find(int organizationId);
        Task<Organization> FindAsync(int organizationId);
        void Save();
        Task SaveAsync();

    }
}