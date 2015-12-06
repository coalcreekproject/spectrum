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

            Property(x => x.Id).HasColumnName("Id").IsOptional();
            Property(x => x.RuleId)
                .HasColumnName("RuleId")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.ParameterId)
                .HasColumnName("ParameterId")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            HasRequired(a => a.Rule).WithMany(b => b.RuleParameters).HasForeignKey(c => c.RuleId);
            HasRequired(a => a.Parameter).WithMany(b => b.RuleParameters).HasForeignKey(c => c.ParameterId);
        }
    }
}