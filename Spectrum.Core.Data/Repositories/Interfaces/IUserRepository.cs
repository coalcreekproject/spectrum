using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Spectrum.Data.Core.Context;
using Spectrum.Data.Core.Models;

namespace Spectrum.Data.Core.Repositories.Interfaces
{
    public interface IUserRepository : IQueryableUserStore<User, int>
    {
        IQueryable<User> All { get; }
        CoreDbContext Context { get; }
        void Delete(int id);
        User Find(int organizationId);
        Task<User> FindAsync(int userId);
        void InsertOrUpdate(User user);
        void Save();
        Task SaveAsync();
    }
}