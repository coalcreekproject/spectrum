using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Configuration
{
    internal class RuleParameterConfiguration : EntityTypeConfiguration<RuleParameter>
    {
        public RuleParameterConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".RuleParameter");
            HasKey(x => new {x.RuleId, x.ParameterId});

            Property(x => x.RuleId)
                .HasColumnName("RuleId")
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

            HasRequired(a => a.Rule).WithMany(b => b.RuleParameters).HasForeignKey(c => c.RuleId);
            HasRequired(a => a.Parameter).WithMany(b => b.RuleParameters).HasForeignKey(c => c.ParameterId);
        }
    }
}