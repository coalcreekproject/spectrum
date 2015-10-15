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
    public class GroupRepository : IGroupRepository
    {
        private CoreDbContext _context;

        public GroupRepository(ICoreUnitOfWork uow)
        {
            _context = uow.Context;
        }

        public IQueryable<Group> All
        {
            get { return _context.Groups; }
        }

        public IQueryable<Group> AllIncluding(params Expression<Func<Group, object>>[] includeProperties)
        {
            IQueryable<Group> query = _context.Groups;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Group Find(int id)
        {
            return _context.Groups.Find(id);
        }

        public void InsertOrUpdate(Group group)
        {
            if (group.Id == default(int)) {
                // New entity
                _context.Groups.Add(group);
            } else {
                // Existing entity
                _context.Entry(group).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var group = _context.Groups.Find(id);
            _context.Groups.Remove(group);
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