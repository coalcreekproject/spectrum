using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Spectrum.Data.Core.Context;
using Spectrum.Data.Core.Context.UnitOfWork;
using Spectrum.Data.Core.Models;
using Spectrum.Data.Core.Repositories.Interfaces;

namespace Spectrum.Data.Core.Repositories
{
    public class OrganizationTypeRepository : IOrganizationTypeRepository
    {
        private readonly CoreDbContext _context;

        public OrganizationTypeRepository(ICoreUnitOfWork uow)
        {
            _context = uow.Context;
        }

        public IQueryable<OrganizationType> All
        {
            get { return _context.OrganizationTypes; }
        }

        public IQueryable<OrganizationType> AllIncluding(
            params Expression<Func<OrganizationType, object>>[] includeProperties)
        {
            IQueryable<OrganizationType> query = _context.OrganizationTypes;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public OrganizationType Find(int id)
        {
            return _context.OrganizationTypes.Find(id);
        }

        public void InsertOrUpdate(OrganizationType organizationtype)
        {
            if (organizationtype.Id == default(int))
            {
                // New entity
                _context.OrganizationTypes.Add(organizationtype);
            }
            else
            {
                // Existing entity
                _context.Entry(organizationtype).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var organizationtype = _context.OrganizationTypes.Find(id);
            _context.OrganizationTypes.Remove(organizationtype);
        }
        public void Save()
        {
            _context.SaveChanges();
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