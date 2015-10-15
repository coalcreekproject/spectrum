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
    public class AreaOfResponsibilityRepository : IAreaOfResponsibilityRepository
    {
        private CoreDbContext _context;

        public AreaOfResponsibilityRepository(ICoreUnitOfWork uow)
        {
            _context = uow.Context;
        }

        public IQueryable<AreaOfResponsibility> All
        {
            get { return _context.AreaOfResponsibilities; }
        }

        public IQueryable<AreaOfResponsibility> AllIncluding(params Expression<Func<AreaOfResponsibility, object>>[] includeProperties)
        {
            IQueryable<AreaOfResponsibility> query = _context.AreaOfResponsibilities;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public AreaOfResponsibility Find(int id)
        {
            return _context.AreaOfResponsibilities.Find(id);
        }

        public void InsertOrUpdate(AreaOfResponsibility areaofresponsibility)
        {
            if (areaofresponsibility.Id == default(int)) {
                // New entity
                _context.AreaOfResponsibilities.Add(areaofresponsibility);
            } else {
                // Existing entity
                _context.Entry(areaofresponsibility).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var areaofresponsibility = _context.AreaOfResponsibilities.Find(id);
            _context.AreaOfResponsibilities.Remove(areaofresponsibility);
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