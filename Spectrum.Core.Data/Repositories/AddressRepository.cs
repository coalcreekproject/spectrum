using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Spectrum.Data.Core.Context;
using Spectrum.Data.Core.Context.UnitOfWork;
using Spectrum.Data.Core.Models;
using Spectrum.Data.Core.Repositories.Interfaces;

namespace Spectrum.Data.Core.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly CoreDbContext _context;

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
            foreach (var includeProperty in includeProperties)
            {
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
            if (addressnorthamerica.Id == default(int))
            {
                // New entity
                _context.Addresses.Add(addressnorthamerica);
            }
            else
            {
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