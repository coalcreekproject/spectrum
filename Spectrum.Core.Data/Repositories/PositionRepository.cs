using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Spectrum.Data.Core.Context;
using Spectrum.Data.Core.Context.UnitOfWork;
using Spectrum.Data.Core.Models;
using Spectrum.Data.Core.Models.Interfaces;
using Spectrum.Data.Core.Repositories.Interfaces;

namespace Spectrum.Data.Core.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        private readonly CoreDbContext _context;

        public PositionRepository(ICoreUnitOfWork uow)
        {
            _context = uow.Context;
        }

        public IQueryable<Position> Positions { get; }

        public IQueryable<Position> All
        {
            get { return _context.Positions; }
        }

        public IQueryable<Position> AllIncluding(params Expression<Func<Position, object>>[] includeProperties)
        {
            IQueryable<Position> query = _context.Positions;

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Position Find(int id)
        {
            return _context.Positions.Find(id);
        }

        public Task<Position> FindAsync(int id)
        {
            return _context.Positions.FirstOrDefaultAsync(p => p.Id.Equals(id));
        }

        public void InsertOrUpdate(Position position)
        {
            if (position.ObjectState == ObjectState.Added)
            {
                // New entity
                _context.Positions.Add(position);
            }
            else
            {
                // Existing entity
                _context.Entry(position).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var position = _context.Positions.Find(id);
            _context.Positions.Remove(position);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Task CreateOrganization(Position position)
        {
            _context.Positions.Add(position);
            return _context.SaveChangesAsync();
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