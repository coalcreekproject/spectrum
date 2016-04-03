using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Spectrum.Data.Core.Context;
using Spectrum.Data.Core.Context.UnitOfWork;
using Spectrum.Data.Core.Models;
using Spectrum.Data.Core.Models.Interfaces;
using Spectrum.Data.Core.Repositories.Interfaces;

namespace Spectrum.Data.Core.Repositories
{
    public class OrganizationUserRepository : IOrganizationUserRepository
    {
        private readonly CoreDbContext _context;

        public OrganizationUserRepository(ICoreUnitOfWork uow)
        {
            _context = uow.Context;
        }

        public IQueryable<UserOrganization> All
        {
            get { return _context.UserOrganizations; }
        }

        public IQueryable<UserOrganization> AllIncluding(
            params Expression<Func<UserOrganization, object>>[] includeProperties)
        {
            IQueryable<UserOrganization> query = _context.UserOrganizations;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public UserOrganization Find(int organizationId)
        {
            return _context.UserOrganizations.Find(organizationId);
        }

        public UserOrganization Find(int organizationId, int userId)
        {
            return _context.UserOrganizations.Find(organizationId, userId);
        }

        public Task<UserOrganization> FindAsync(int organizationId)
        {
            return _context.UserOrganizations.FirstOrDefaultAsync(uo => uo.OrganizationId.Equals(organizationId));
        }

        public Task<UserOrganization> FindAsync(int organizationId, int userId)
        {
            return _context.UserOrganizations.FirstOrDefaultAsync(uo => uo.OrganizationId.Equals(organizationId) && uo.UserId.Equals(userId));
        }

        public void InsertOrUpdate(UserOrganization userOrganization)
        {
            if (userOrganization.ObjectState == ObjectState.Added)
            {
                // New entity
                _context.UserOrganizations.Add(userOrganization);
            }
            else
            {
                // Existing entity
                _context.Entry(userOrganization).State = EntityState.Modified;
            }
        }

        public void Delete(int organizationId, int userId)
        {
            //var userOrganization = _context.UserOrganizations.Find(organizationId, userId);
            var userOrganization = _context.UserOrganizations.FirstOrDefault(uo => uo.OrganizationId.Equals(organizationId) && uo.UserId.Equals(userId));

            if (userOrganization != null)
                _context.UserOrganizations.Remove(userOrganization);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && _context != null)
            {
                _context.Dispose();
            }
        }
    }
}