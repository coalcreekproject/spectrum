using System.Threading.Tasks;
using Spectrum.Data.Core.Models;

namespace Spectrum.Data.Core.Repositories.Interfaces
{
    public interface IPositionRepository : IEntityRepository<Position>
    {
        Position Find(int positionId);
        Task<Position> FindAsync(int positionId);
        void Save();
        Task SaveAsync();
    }
}