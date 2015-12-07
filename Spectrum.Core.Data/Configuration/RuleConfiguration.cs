using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Configuration
{
    internal class RuleConfiguration : EntityTypeConfiguration<Rule>
    {
        public RuleConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Rule");
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("Id")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.OrganizationId).HasColumnName("OrganizationId").IsRequired();
            Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(128);
            Property(x => x.Description).HasColumnName("Description").IsOptional().HasMaxLength(256);
            Property(x => x.RuleTypeId).HasColumnName("RuleTypeId").IsRequired();

            HasRequired(a => a.Organization).WithMany(b => b.Rules).HasForeignKey(c => c.OrganizationId);
            HasRequired(a => a.RuleType).WithMany(b => b.Rules).HasForeignKey(c => c.RuleTypeId);
        }
    }
}