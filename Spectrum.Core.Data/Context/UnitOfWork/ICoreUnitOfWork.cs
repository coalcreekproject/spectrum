using System;
using System.Threading.Tasks;

namespace Spectrum.Data.Core.Context.UnitOfWork
{
    public interface ICoreUnitOfWork : IDisposable
    {
        CoreDbContext Context { get; }
        int Save();
        Task<int> SaveAsync();
        //void Dispose();
    }
}