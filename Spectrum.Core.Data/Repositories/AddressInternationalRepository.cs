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
    public class AddressInternationalRepository : IEntityRepository<AddressInternational>
    {
        private CoreDbContext _context;

        public AddressInternationalRepository(ICoreUnitOfWork uow)
        {
            _context = uow.Context;
        }

        public IQueryable<AddressInternational> All
        {
            get { return _context.AddressInternationals; }
        }

        public IQueryable<AddressInternational> AllIncluding(params Expression<Func<AddressInternational, object>>[] includeProperties)
        {
            IQueryable<AddressInternational> query = _context.AddressInternationals;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public AddressInternational Find(int id)
        {
            return _context.AddressInternationals.Find(id);
        }

        /// <summary>
        /// This is to add a graph, or cascading objects associated
        /// </summary>
        /// <param name="addressinternational"></param>
        public void AddGraph(AddressInternational addressinternational)
        {
            _context.AddressInternationals.Add(addressinternational);
        }

        public void InsertOrUpdate(AddressInternational addressinternational)
        {
            if (addressinternational.Id == default(int)) {
                // New entity
                _context.Entry(addressinternational).State = EntityState.Added;
            } else {
                // Existing entity
                _context.Entry(addressinternational).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var addressinternational = _context.AddressInternationals.Find(id);
            _context.AddressInternationals.Remove(addressinternational);
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