using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Data.Core.Models;

namespace Spectrum.Data.Core.Configuration
{
    internal class OrganizationPreferenceConfiguration : EntityTypeConfiguration<OrganizationPreference>
    {
        public OrganizationPreferenceConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".OrganizationPreference");
            HasKey(x => new {x.OrganizationId, x.PreferenceId});

            Property(x => x.OrganizationId)
                .HasColumnName("OrganizationId")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.PreferenceId)
                .HasColumnName("PreferenceId")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            HasRequired(a => a.Organization)
                .WithMany(b => b.OrganizationPreferences)
                .HasForeignKey(c => c.OrganizationId);
            HasRequired(a => a.Preference).WithMany(b => b.OrganizationPreferences).HasForeignKey(c => c.PreferenceId);
        }
    }
}