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
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly CoreDbContext _context;

        public OrganizationRepository(ICoreUnitOfWork uow)
        {
            _context = uow.Context;
        }

        public IQueryable<Organization> Organizations
        {
            get { return _context.Organizations; }
        }

        public IQueryable<Organization> All
        {
            get { return _context.Organizations; }
        }

        public IQueryable<Organization> AllIncluding(params Expression<Func<Organization, object>>[] includeProperties)
        {
            IQueryable<Organization> query = _context.Organizations;
            foreach (var includeProperty in includeProperties)
            {
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

        public Task UpdateAsync(Organization organization)
        {
            _context.Entry(organization).State = EntityState.Modified;
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