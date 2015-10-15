using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Configuration
{
    // OrganizationApplications
    internal partial class OrganizationApplicationConfiguration : EntityTypeConfiguration<OrganizationApplication>
    {
        public OrganizationApplicationConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".OrganizationApplications");
            HasKey(x => new { x.OrganizationId, x.ApplicationId });

            Property(x => x.OrganizationId).HasColumnName("OrganizationId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.ApplicationId).HasColumnName("ApplicationId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Key).HasColumnName("Key").IsOptional().HasMaxLength(256);

            // Foreign keys
            HasRequired(a => a.Organization).WithMany(b => b.OrganizationApplications).HasForeignKey(c => c.OrganizationId); // FK_OrganizationApplications_Organization
            HasRequired(a => a.Application).WithMany(b => b.OrganizationApplications).HasForeignKey(c => c.ApplicationId); // FK_OrganizationApplications_Application
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
