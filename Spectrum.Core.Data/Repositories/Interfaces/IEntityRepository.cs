using System;
using System.Linq;
using System.Linq.Expressions;

namespace Spectrum.Core.Data.Repositories.Interfaces
{
    public interface IEntityRepository<T> : IDisposable
    {
        IQueryable<T> All { get; }
        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        T Find(int id);
        void InsertOrUpdate(T entity);
        void Delete(int id);

        //void Save();
    }
}