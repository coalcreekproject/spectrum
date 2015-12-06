using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Spectrum.Core.Data.Context;
using Spectrum.Core.Data.Context.UnitOfWork;
using Spectrum.Core.Data.Models;
using Spectrum.Core.Data.Repositories.Interfaces;

namespace Spectrum.Core.Data.Repositories
{
    public class ApplicationProfileRepository : IApplicationProfileRepository
    {
        private readonly CoreDbContext _context;

        public ApplicationProfileRepository(ICoreUnitOfWork uow)
        {
            _context = uow.Context;
        }

        public IQueryable<ApplicationProfile> All
        {
            get { return _context.ApplicationProfiles; }
        }

        public IQueryable<ApplicationProfile> AllIncluding(
            params Expression<Func<ApplicationProfile, object>>[] includeProperties)
        {
            IQueryable<ApplicationProfile> query = _context.ApplicationProfiles;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public ApplicationProfile Find(int id)
        {
            return _context.ApplicationProfiles.Find(id);
        }

        public void InsertOrUpdate(ApplicationProfile applicationprofile)
        {
            if (applicationprofile.Id == default(int))
            {
                // New entity
                _context.ApplicationProfiles.Add(applicationprofile);
            }
            else
            {
                // Existing entity
                _context.Entry(applicationprofile).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var applicationprofile = _context.ApplicationProfiles.Find(id);
            _context.ApplicationProfiles.Remove(applicationprofile);
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