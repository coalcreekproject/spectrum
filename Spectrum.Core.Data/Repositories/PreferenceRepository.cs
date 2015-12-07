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
    public class PreferenceRepository : IPreferenceRepository
    {
        private readonly CoreDbContext _context;

        public PreferenceRepository(ICoreUnitOfWork uow)
        {
            _context = uow.Context;
        }

        public IQueryable<Preference> All
        {
            get { return _context.Preferences; }
        }

        public IQueryable<Preference> AllIncluding(params Expression<Func<Preference, object>>[] includeProperties)
        {
            IQueryable<Preference> query = _context.Preferences;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Preference Find(int id)
        {
            return _context.Preferences.Find(id);
        }

        public void InsertOrUpdate(Preference preference)
        {
            if (preference.Id == default(int))
            {
                // New entity
                _context.Preferences.Add(preference);
            }
            else
            {
                // Existing entity
                _context.Entry(preference).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var preference = _context.Preferences.Find(id);
            _context.Preferences.Remove(preference);
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