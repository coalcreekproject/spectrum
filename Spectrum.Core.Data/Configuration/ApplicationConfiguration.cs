using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Configuration
{
    // Application
    internal partial class ApplicationConfiguration : EntityTypeConfiguration<Application>
    {
        public ApplicationConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Application");
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("Id")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(128);
            HasMany(t => t.Parameters).WithMany(t => t.Applications).Map(m =>
            {
                m.ToTable("ApplicationParameter", schema);
                m.MapLeftKey("ApplicationId");
                m.MapRightKey("ParameterId");
            });
            InitializePartial();
        }

        partial void InitializePartial();
    }
}
