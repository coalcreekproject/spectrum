using System.Threading.Tasks;
using Spectrum.Core.Data.Context.Extensions;

namespace Spectrum.Core.Data.Context.UnitOfWork
{
    public class CoreUnitOfWork : ICoreUnitOfWork
    {
        private readonly CoreDbContext _context;

        public CoreUnitOfWork()
        {
            _context = new CoreDbContext();
        }

        public CoreUnitOfWork(CoreDbContext context)
        {
            _context = context;
        }

        public CoreDbContext Context
        {
            get { return _context; }
        }

        public int Save()
        {
            _context.ApplyObjectStateChanges();
            return _context.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            _context.ApplyObjectStateChanges();
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
