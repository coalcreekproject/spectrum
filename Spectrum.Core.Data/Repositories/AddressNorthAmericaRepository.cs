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
    public class AddressNorthAmericaRepository : IAddressNorthAmericaRepository
    {
        private CoreDbContext _context;

        public AddressNorthAmericaRepository(ICoreUnitOfWork uow)
        {
            _context = uow.Context;
        }

        public IQueryable<AddressNorthAmerica> All
        {
            get { return _context.AddressNorthAmericas; }
        }

        public IQueryable<AddressNorthAmerica> AllIncluding(params Expression<Func<AddressNorthAmerica, object>>[] includeProperties)
        {
            IQueryable<AddressNorthAmerica> query = _context.AddressNorthAmericas;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public AddressNorthAmerica Find(int id)
        {
            return _context.AddressNorthAmericas.Find(id);
        }

        public void InsertOrUpdate(AddressNorthAmerica addressnorthamerica)
        {
            if (addressnorthamerica.Id == default(int)) {
                // New entity
                _context.AddressNorthAmericas.Add(addressnorthamerica);
            } else {
                // Existing entity
                _context.Entry(addressnorthamerica).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var addressnorthamerica = _context.AddressNorthAmericas.Find(id);
            _context.AddressNorthAmericas.Remove(addressnorthamerica);
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