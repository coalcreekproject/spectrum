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
    public class ApplicationRepository : IEntityRepository<Application>
    {
        private readonly CoreDbContext _context;

        public ApplicationRepository(ICoreUnitOfWork uow)
        {
            _context = uow.Context;
        }

        public IQueryable<Application> All
        {
            get { return _context.Applications; }
        }

        public IQueryable<Application> AllIncluding(params Expression<Func<Application, object>>[] includeProperties)
        {
            IQueryable<Application> query = _context.Applications;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Application Find(int id)
        {
            return _context.Applications.Find(id);
        }

        public void InsertOrUpdate(Application application)
        {
            if (application.Id == default(int))
            {
                // New entity
                _context.Entry(application).State = EntityState.Added;
            }
            else
            {
                // Existing entity
                _context.Entry(application).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var application = _context.Applications.Find(id);
            _context.Applications.Remove(application);
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