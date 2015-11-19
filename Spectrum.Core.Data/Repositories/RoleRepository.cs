using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Spectrum.Core.Data.Context;
using Spectrum.Core.Data.Context.UnitOfWork;
using Spectrum.Core.Data.Models;
using Spectrum.Core.Data.Models.Interfaces;
using Spectrum.Core.Data.Repositories.Interfaces;

namespace Spectrum.Core.Data.Repositories
{
    public class RoleRepository : IQueryableRoleStore<Role, int>, IRoleRepository
    {
        private CoreDbContext _context;

        public RoleRepository(ICoreUnitOfWork uow)
        {
            _context = uow.Context;
        }

        public IQueryable<Role> All
        {
            get { return _context.Roles; }
        }

        public IQueryable<Role> AllIncluding(params Expression<Func<Role, object>>[] includeProperties)
        {
            IQueryable<Role> query = _context.Roles;

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Role Find(int id)
        {
            return _context.Roles.Find(id);
        }

        public Task<Role> FindAsync(int id)
        {
            return _context.Roles.FirstOrDefaultAsync(p => p.Id.Equals(id));
        }

        public void InsertOrUpdate(Role role)
        {
            if (role.ObjectState == ObjectState.Added)
            {
                // New entity
                _context.Roles.Add(role);
            }
            else
            {
                // Existing entity
                _context.Entry(role).State = EntityState.Modified;
            }
        }

        public Task CreateOrganization(Role role)
        {
            this._context.Roles.Add(role);
            return this._context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var role = _context.Roles.Find(id);
            _context.Roles.Remove(role);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        #region IQueryableRoleStore Implementation for ASP.NET Identity 2.0

        public Task CreateAsync(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            this._context.Roles.Add(role);
            return this._context.SaveChangesAsync();
        }

        public Task UpdateAsync(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            this._context.Entry(role).State = EntityState.Modified;
            return this._context.SaveChangesAsync();
        }

        public Task DeleteAsync(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            this._context.Roles.Remove(role);
            return this._context.SaveChangesAsync();
        }

        public Task<Role> FindByIdAsync(int roleId)
        {
            return Roles.FirstOrDefaultAsync(r => r.Id.Equals(roleId));
        }

        public Task<Role> FindByNameAsync(string roleName)
        {
            return Roles.FirstOrDefaultAsync(r => r.Name.Equals(roleName));
        }

        #endregion

        public void Dispose()
        {
            _context.Dispose();
        }

        public IQueryable<Role> Roles { get; }
    }
}
