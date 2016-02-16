using System.Threading.Tasks;
using Spectrum.Data.Core.Context;
using Spectrum.Data.Core.Context.Interfaces;
using Spectrum.Data.Core.Context.UnitOfWork;
using Spectrum.Data.Core.Models;

namespace Spectrum.Data.Core.Repositories.Interfaces
{
    public interface IRoleRepository : IEntityRepository<Role>
    {
        Role Find(int roleId);
        Task<Role> FindAsync(int roleId);
        void Save();
        Task SaveAsync();
        CoreDbContext Context { get; }
        ICoreUnitOfWork UnitOfWork { get; }
    }
}