using System.Threading.Tasks;
using Spectrum.Data.Core.Models;

namespace Spectrum.Data.Core.Repositories.Interfaces
{
    public interface IUserProfileRepository : IEntityRepository<UserProfile>
    {
        UserProfile Find(int userProfileId);
        Task<UserProfile> FindAsync(int userProfileId);
        void Save();
        Task SaveAsync();
    }
}