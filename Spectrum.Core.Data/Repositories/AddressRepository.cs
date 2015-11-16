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
    public class AddressRepository : IAddressNorthAmericaRepository
    {
        private CoreDbContext _context;

        public AddressRepository(ICoreUnitOfWork uow)
        {
            _context = uow.Context;
        }

        public IQueryable<Address> All
        {
            get { return _context.Addresses; }
        }

        public IQueryable<Address> AllIncluding(params Expression<Func<Address, object>>[] includeProperties)
        {
            IQueryable<Address> query = _context.Addresses;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Address Find(int id)
        {
            return _context.Addresses.Find(id);
        }

        public void InsertOrUpdate(Address addressnorthamerica)
        {
            if (addressnorthamerica.Id == default(int)) {
                // New entity
                _context.Addresses.Add(addressnorthamerica);
            } else {
                // Existing entity
                _context.Entry(addressnorthamerica).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var addressnorthamerica = _context.Addresses.Find(id);
            _context.Addresses.Remove(addressnorthamerica);
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