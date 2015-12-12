using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Data.Core.Models;

namespace Spectrum.Data.Core.Configuration
{
    internal class JurisdictionProfileConfiguration : EntityTypeConfiguration<JurisdictionProfile>
    {
        public JurisdictionProfileConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".JurisdictionProfile");
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("Id")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.JurisdictionId).HasColumnName("JurisdictionId").IsRequired();
            Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(128);
            Property(x => x.Description).HasColumnName("Description").IsOptional();

            HasRequired(a => a.Jurisdiction).WithMany(b => b.JurisdictionProfiles).HasForeignKey(c => c.JurisdictionId);
        }
    }
}