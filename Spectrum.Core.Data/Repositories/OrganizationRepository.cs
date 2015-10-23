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

        public Task<Organization> FindAsync(int organizationId)
        {
            return _context.Organizations.FirstOrDefaultAsync(o => o.Id.Equals(organizationId));
        }

        public void InsertOrUpdate(Organization organization)
        {
            if (organization.ObjectState == ObjectState.Added)
            {
                // New entity
                _context.Organizations.Add(organization);
            }
            else
            {
                // Existing entity
                _context.Entry(organization).State = EntityState.Modified;
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

        public void Delete(int id)
        {
            var organization = _context.Organizations.Find(id);
            _context.Organizations.Remove(organization);
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
            _context.Dispose();
        }
    }
}