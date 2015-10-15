using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Spectrum.Core.Data.Context;
using Spectrum.Core.Data.Context.UnitOfWork;
using Spectrum.Core.Data.Models;
using Spectrum.Core.Data.Models.Interfaces;
using Spectrum.Core.Data.Repositories.Interfaces;

namespace Spectrum.Core.Data.Repositories
{ 
    public class UserProfileRepository : IUserProfileRepository
    {
        private CoreDbContext _context;

        public UserProfileRepository(ICoreUnitOfWork uow)
        {
            _context = uow.Context;
        }
        
        public IQueryable<UserProfile> All
        {
            get { return _context.UserProfiles; }
        }

        public IQueryable<UserProfile> AllIncluding(params Expression<Func<UserProfile, object>>[] includeProperties)
        {
            IQueryable<UserProfile> query = _context.UserProfiles;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public UserProfile Find(int id)
        {
            return _context.UserProfiles.Find(id);
        }

        public void InsertOrUpdate(UserProfile userprofile)
        {
            if (userprofile.ObjectState == ObjectState.Added) {
                // New entity
                _context.UserProfiles.Add(userprofile);
                Save();
            } else {
                // Existing entity
                _context.Entry(userprofile).State = EntityState.Modified;
                Save();
            }
        }

        public void Delete(int id)
        {
            var userprofile = _context.UserProfiles.Find(id);
            _context.UserProfiles.Remove(userprofile);
            Save();
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