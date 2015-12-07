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
    public class ParameterRepository : IParameterRepository
    {
        private readonly CoreDbContext _context;

        public ParameterRepository(ICoreUnitOfWork uow)
        {
            _context = uow.Context;
        }

        public IQueryable<Parameter> All
        {
            get { return _context.Parameters; }
        }

        public IQueryable<Parameter> AllIncluding(params Expression<Func<Parameter, object>>[] includeProperties)
        {
            IQueryable<Parameter> query = _context.Parameters;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Parameter Find(int id)
        {
            return _context.Parameters.Find(id);
        }

        public void InsertOrUpdate(Parameter parameter)
        {
            if (parameter.Id == default(int))
            {
                // New entity
                _context.Parameters.Add(parameter);
            }
            else
            {
                // Existing entity
                _context.Entry(parameter).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var parameter = _context.Parameters.Find(id);
            _context.Parameters.Remove(parameter);
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