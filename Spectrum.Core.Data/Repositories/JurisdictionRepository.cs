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
    public class JurisdictionRepository : IJurisdictionRepository
    {
        private readonly CoreDbContext _context;

        public JurisdictionRepository(ICoreUnitOfWork uow)
        {
            _context = uow.Context;
        }

        public IQueryable<Jurisdiction> All
        {
            get { return _context.Jurisdictions; }
        }

        public IQueryable<Jurisdiction> AllIncluding(params Expression<Func<Jurisdiction, object>>[] includeProperties)
        {
            IQueryable<Jurisdiction> query = _context.Jurisdictions;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Jurisdiction Find(int id)
        {
            return _context.Jurisdictions.Find(id);
        }

        public void InsertOrUpdate(Jurisdiction jurisdiction)
        {
            if (jurisdiction.Id == default(int))
            {
                // New entity
                _context.Jurisdictions.Add(jurisdiction);
            }
            else
            {
                // Existing entity
                _context.Entry(jurisdiction).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var jurisdiction = _context.Jurisdictions.Find(id);
            _context.Jurisdictions.Remove(jurisdiction);
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