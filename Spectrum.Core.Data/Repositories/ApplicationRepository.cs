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
        private CoreDbContext _context;

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

        /// <summary>
        /// This is to add a graph, or cascading objects associated
        /// </summary>
        /// <param name="application"></param>
        public void AddGraph(Application application)
        {
            _context.Applications.Add(application);
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

        //public void Save()
        //{
        //    context.SaveChanges();
        //}

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}