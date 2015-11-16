using System.Threading.Tasks;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Repositories.Interfaces
{
    public interface IUserProfileRepository : IEntityRepository<UserProfile>
    {
        UserProfile Find(int userProfileId);
        Task<UserProfile> FindAsync(int userProfileId);
        void Save();
        Task SaveAsync();
    }
}