using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Data.Core.Models;

namespace Spectrum.Data.Core.Configuration
{
    internal class ApplicationParameterConfiguration : EntityTypeConfiguration<ApplicationParameter>
    {
        public ApplicationParameterConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".ApplicationParameter");
            HasKey(x => new {x.ApplicationId, x.ParameterId});

            Property(x => x.Id).HasColumnName("Id").IsOptional();
            Property(x => x.ApplicationId)
                .HasColumnName("ApplicationId")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.ParameterId)
                .HasColumnName("ParameterId")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            HasRequired(a => a.Application).WithMany(b => b.ApplicationParameters).HasForeignKey(c => c.ApplicationId);
            HasRequired(a => a.Parameter).WithMany(b => b.ApplicationParameters).HasForeignKey(c => c.ParameterId);
        }
    }
}