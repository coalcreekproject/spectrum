using System.Collections.Generic;
using System.Threading.Tasks;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Repositories.Interfaces
{
    public interface IRoleRepository : IEntityRepository<Role>
    {
        Role Find(int roleId);
        Task<Role> FindAsync(int roleId);
        void Save();
        Task SaveAsync();
    }
}