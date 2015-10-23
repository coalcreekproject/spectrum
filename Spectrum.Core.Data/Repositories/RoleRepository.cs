using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Spectrum.Core.Data.Context;
using Spectrum.Core.Data.Context.UnitOfWork;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Repositories
{
    public class RoleRepository : IQueryableRoleStore<Role, int>, IRoleRepository
    {
        private readonly CoreDbContext _context;

        public RoleRepository(ICoreUnitOfWork uow)
        {
            _context = uow.Context;
        }

        public IQueryable<Role> Roles
        {
            get
            {
                return _context.Roles;
            }
        }

        public virtual Task CreateAsync(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            this._context.Roles.Add(role);
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

        public Task UpdateAsync(Role role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            this._context.Entry(role).State = EntityState.Modified;
            return this._context.SaveChangesAsync();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && this._context != null)
            {
                this._context.Dispose();
            }
        }
    }
}
