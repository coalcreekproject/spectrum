using System;
using System.Threading.Tasks;

namespace Spectrum.Core.Data.Context.UnitOfWork
{
    public interface ICoreUnitOfWork : IDisposable
    {
        CoreDbContext Context { get; }
        int Save();
        Task<int> SaveAsync();
        //void Dispose();
    }
}