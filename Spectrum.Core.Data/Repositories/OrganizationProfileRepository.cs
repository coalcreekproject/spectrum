using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Spectrum.Core.Data.Context;
using Spectrum.Core.Data.Context.UnitOfWork;
using Spectrum.Core.Data.Models;
using Spectrum.Core.Data.Repositories.Interfaces;
using System.Threading.Tasks;
using Spectrum.Core.Data.Models.Interfaces;

namespace Spectrum.Core.Data.Repositories
{ 
    public class OrganizationProfileRepository : IOrganizationProfileRepository
    {
        private CoreDbContext _context;

        public OrganizationProfileRepository(ICoreUnitOfWork uow)
        {
            _context = uow.Context;
        }

        public IQueryable<OrganizationProfile> All
        {
            get { return _context.OrganizationProfiles; }
        }

        public IQueryable<OrganizationProfile> AllIncluding(params Expression<Func<OrganizationProfile, object>>[] includeProperties)
        {
            IQueryable<OrganizationProfile> query = _context.OrganizationProfiles;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public OrganizationProfile Find(int id)
        {
            return _context.OrganizationProfiles.Find(id);
        }

        public Task<OrganizationProfile> FindAsync(int id)
        {
            return _context.OrganizationProfiles.FirstOrDefaultAsync(p => p.Id.Equals(id));
        }

        public void InsertOrUpdate(OrganizationProfile organizationProfile)
        {
            if (organizationProfile.ObjectState == ObjectState.Added)
            {
                // New entity
                _context.OrganizationProfiles.Add(organizationProfile);
            }
            else
            {
                // Existing entity
                //_context.Entry(organizationProfile.Organization).State = EntityState.Unchanged;
                _context.Entry(organizationProfile).State = EntityState.Modified;
            }
        }

        public Task CreateOrganization(OrganizationProfile organizationProfile)
        {
            this._context.OrganizationProfiles.Add(organizationProfile);
            return this._context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var organizationprofile = _context.OrganizationProfiles.Find(id);
            _context.OrganizationProfiles.Remove(organizationprofile);
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