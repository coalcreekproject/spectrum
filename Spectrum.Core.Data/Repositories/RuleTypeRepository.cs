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
    public class RuleTypeRepository : IRuleTypeRepository
    {
        private CoreDbContext _context;

        public RuleTypeRepository(ICoreUnitOfWork uow)
        {
            _context = uow.Context;
        }

        public IQueryable<RuleType> All
        {
            get { return _context.RuleTypes; }
        }

        public IQueryable<RuleType> AllIncluding(params Expression<Func<RuleType, object>>[] includeProperties)
        {
            IQueryable<RuleType> query = _context.RuleTypes;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public RuleType Find(int id)
        {
            return _context.RuleTypes.Find(id);
        }

        public void InsertOrUpdate(RuleType ruletype)
        {
            if (ruletype.Id == default(int)) {
                // New entity
                _context.RuleTypes.Add(ruletype);
            } else {
                // Existing entity
                _context.Entry(ruletype).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var ruletype = _context.RuleTypes.Find(id);
            _context.RuleTypes.Remove(ruletype);
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