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
    public class RuleRepository : IRuleRepository
    {
        private readonly CoreDbContext _context;

        public RuleRepository(ICoreUnitOfWork uow)
        {
            _context = uow.Context;
        }

        public IQueryable<Rule> All
        {
            get { return _context.Rules; }
        }

        public IQueryable<Rule> AllIncluding(params Expression<Func<Rule, object>>[] includeProperties)
        {
            IQueryable<Rule> query = _context.Rules;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Rule Find(int id)
        {
            return _context.Rules.Find(id);
        }

        public void InsertOrUpdate(Rule rule)
        {
            if (rule.Id == default(int))
            {
                // New entity
                _context.Rules.Add(rule);
            }
            else
            {
                // Existing entity
                _context.Entry(rule).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var rule = _context.Rules.Find(id);
            _context.Rules.Remove(rule);
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