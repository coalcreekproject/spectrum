using System.Threading.Tasks;
using Spectrum.Data.Core.Models;

namespace Spectrum.Data.Core.Repositories.Interfaces
{
    public interface IRoleRepository : IEntityRepository<Role>
    {
        Role Find(int roleId);
        Task<Role> FindAsync(int roleId);
        void Save();
        Task SaveAsync();
    }
}