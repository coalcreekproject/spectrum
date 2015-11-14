using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Configuration
{
    internal class ApplicationParameterConfiguration : EntityTypeConfiguration<ApplicationParameter>
    {
        public ApplicationParameterConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".ApplicationParameter");
            HasKey(x => new {x.ApplicationId, x.ParameterId});

            Property(x => x.ApplicationId)
                .HasColumnName("ApplicationId")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.ParameterId)
                .HasColumnName("ParameterId")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Cloaked).HasColumnName("Cloaked").IsOptional();
            Property(x => x.Archive).HasColumnName("Archive").IsOptional();
            Property(x => x.CreatedDate).HasColumnName("CreatedDate").IsOptional();
            Property(x => x.CreatedByUserId).HasColumnName("CreatedByUserId").IsOptional();
            Property(x => x.ModifiedDate).HasColumnName("ModifiedDate").IsOptional();
            Property(x => x.ModifiedByUserId).HasColumnName("ModifiedByUserId").IsOptional();

            HasRequired(a => a.Application).WithMany(b => b.ApplicationParameters).HasForeignKey(c => c.ApplicationId);
            HasRequired(a => a.Parameter).WithMany(b => b.ApplicationParameters).HasForeignKey(c => c.ParameterId);
        }
    }
}