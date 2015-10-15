using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Spectrum.Core.Data.Context;
using Spectrum.Core.Data.Context.UnitOfWork;
using Spectrum.Core.Data.Models;
using Spectrum.Core.Data.Models.Interfaces;
using Spectrum.Core.Data.Repositories.Interfaces;

namespace Spectrum.Core.Data.Repositories
{ 
    public class OrganizationRepository : IOrganizationRepository
    {
        private CoreDbContext _context;

        public OrganizationRepository(ICoreUnitOfWork uow)
        {
            _context = uow.Context;
        }

        public IQueryable<Organization> All
        {
            get { return _context.Organizations; }
        }

        public IQueryable<Organization> AllIncluding(params Expression<Func<Organization, object>>[] includeProperties)
        {
            IQueryable<Organization> query = _context.Organizations;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Organization Find(int id)
        {
            return _context.Organizations.Find(id);
        }

        public void InsertOrUpdate(Organization organization)
        {
            if (organization.ObjectState == ObjectState.Added) {
                // New entity
                _context.Organizations.Add(organization);
                Save();
            } else {
                // Existing entity
                _context.Entry(organization).State = EntityState.Modified;
                Save();
            }
        }

        public IQueryable<Organization> Organizations
        {
            get { return _context.Organizations; }
        }


        public Task CreateOrganization(Organization organization)
        {
            this._context.Organizations.Add(organization);
            return this._context.SaveChangesAsync();
        }

        public Task UpdateAsync(Organization organization)
        {
            this._context.Entry<Organization>(organization).State = EntityState.Modified;
            return this._context.SaveChangesAsync();
        }

        public Task DeleteAsync(Organization organization)
        {
            this._context.Organizations.Remove(organization);
            return this._context.SaveChangesAsync();
        }

        public Task<Organization> FindByIdAsync(int organizationId)
        {
            return Organizations.FirstOrDefaultAsync(o => o.Id.Equals(organizationId));
        }

        public Task<Organization> FindByNameAsync(string organizationName)
        {
            return Organizations.FirstOrDefaultAsync(o => o.Name.Equals(organizationName));
        }

        public void Delete(int id)
        {
            var organization = _context.Organizations.Find(id);
            _context.Organizations.Remove(organization);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}