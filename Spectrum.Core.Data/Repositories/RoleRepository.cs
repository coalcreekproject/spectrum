using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Spectrum.Data.Core.Context;
using Spectrum.Data.Core.Context.Interfaces;
using Spectrum.Data.Core.Context.UnitOfWork;
using Spectrum.Data.Core.Models;
using Spectrum.Data.Core.Models.Interfaces;
using Spectrum.Data.Core.Repositories.Interfaces;

namespace Spectrum.Data.Core.Repositories
{
    public class RoleRepository : IQueryableRoleStore<Role, int>, IRoleRepository
    {
        public RoleRepository(ICoreUnitOfWork uow)
        {
            UnitOfWork = uow;
            Context = uow.Context;
        }

        public CoreDbContext Context { get; }

        public ICoreUnitOfWork UnitOfWork { get; }

        public IQueryable<Role> Roles { get; }

        public IQueryable<Role> All
        {
            get { return Context.Roles; }
        }

        public IQueryable<Role> AllIncluding(params Expression<Func<Role, object>>[] includeProperties)
        {
            IQueryable<Role> query = Context.Roles;

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Role Find(int id)
        {
            return Context.Roles.Find(id);
        }

        public Task<Role> FindAsync(int id)
        {
            return Context.Roles.FirstOrDefaultAsync(p => p.Id.Equals(id));
        }

        public void InsertOrUpdate(Role role)
        {
            if (role.ObjectState == ObjectState.Added)
            {
                // New entity
                Context.Roles.Add(role);
            }
            else
            {
                // Existing entity
                Context.Entry(role).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var role = Context.Roles.Find(id);
            Context.Roles.Remove(role);
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public Task SaveAsync()
        {
            return Context.SaveChangesAsync();
        }

        public Task CreateOrganization(Role role)
        {
            Context.Roles.Add(role);
            return Context.SaveChangesAsync();
        }

        #region IQueryableRoleStore Implementation for ASP.NET Identity 2.0

        public Task CreateAsync(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            Context.Roles.Add(role);
            return Context.SaveChangesAsync();
        }

        public Task UpdateAsync(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            Context.Entry(role).State = EntityState.Modified;
            return Context.SaveChangesAsync();
        }

        public Task DeleteAsync(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            Context.Roles.Remove(role);
            return Context.SaveChangesAsync();
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
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && Context != null)
            {
                Context.Dispose();
            }
        }
    }
}