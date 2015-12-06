using System.Data.Entity;
using Spectrum.Core.Data.Context.Initializers;

namespace Spectrum.Core.Data.Context
{
    public partial class CoreDbContext
    {
        public CoreDbContext()
        {
            //Debug.Write(Database.Connection.ConnectionString);

            Database.SetInitializer(new CoreInitializer());

            //Database.SetInitializer<CoreDbContext>(new CreateDatabaseIfNotExists<CoreDbContext>());
            //Database.SetInitializer<TContext>(new DropCreateDatabaseIfModelChanges<SpectrumCoreContext>());
            //Database.SetInitializer<TContext>(new DropCreateDatabaseAlways<SpectrumCoreContext>());
            //Database.SetInitializer<TContext>(null);
        }
    }
}