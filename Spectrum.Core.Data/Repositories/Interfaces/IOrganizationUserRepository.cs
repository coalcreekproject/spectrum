using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Spectrum.Data.Core.Models;

namespace Spectrum.Data.Core.Repositories.Interfaces
{
    public interface IOrganizationUserRepository
    {
        IQueryable<UserOrganization> All { get; }

        IQueryable<UserOrganization> AllIncluding(params Expression<Func<UserOrganization, object>>[] includeProperties);
        void Delete(int organizationId, int userId);
        void Dispose();
        UserOrganization Find(int organizationId);
        UserOrganization Find(int organizationId, int userId);
        Task<UserOrganization> FindAsync(int organizationId);
        Task<UserOrganization> FindAsync(int organizationId, int userId);
        void InsertOrUpdate(UserOrganization userOrganization);
        void Save();
        Task SaveAsync();
    }
}