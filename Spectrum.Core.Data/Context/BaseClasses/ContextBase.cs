using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Spectrum.Core.Data.Context.BaseClasses
{
    public partial class ContextBase<TContext> : DbContext where TContext : DbContext
    {
        /// <summary>
        ///     All of our context connections will use this database
        /// </summary>
        protected ContextBase() : base("name=SpectrumCore")
        {
            InitializePartial();
        }

        public ContextBase(string connectionString) : base(connectionString)
        {
            InitializePartial();
        }

        public ContextBase(string connectionString, DbCompiledModel model)
            : base(connectionString, model)
        {
            InitializePartial();
        }

        partial void InitializePartial();
        partial void OnModelCreatingPartial(DbModelBuilder modelBuilder);
    }
}