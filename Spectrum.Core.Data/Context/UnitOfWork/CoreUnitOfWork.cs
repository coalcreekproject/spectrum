using System.Threading.Tasks;
using Spectrum.Core.Data.Context.Extensions;

namespace Spectrum.Core.Data.Context.UnitOfWork
{
    public class CoreUnitOfWork : ICoreUnitOfWork
    {
        public CoreUnitOfWork()
        {
            Context = new CoreDbContext();
        }

        public CoreUnitOfWork(CoreDbContext context)
        {
            Context = context;
        }

        public CoreDbContext Context { get; }

        public int Save()
        {
            Context.ApplyObjectStateChanges();
            return Context.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            Context.ApplyObjectStateChanges();
            return Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}