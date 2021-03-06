using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Data.Core.Models;

namespace Spectrum.Data.Core.Configuration
{
    internal class OrganizationApplicationConfiguration : EntityTypeConfiguration<OrganizationApplication>
    {
        public OrganizationApplicationConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".OrganizationApplications");
            HasKey(x => new {x.OrganizationId, x.ApplicationId});

            Property(x => x.OrganizationId)
                .HasColumnName("OrganizationId")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.ApplicationId)
                .HasColumnName("ApplicationId")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Key).HasColumnName("Key").IsRequired().HasMaxLength(256);

            HasRequired(a => a.Organization)
                .WithMany(b => b.OrganizationApplications)
                .HasForeignKey(c => c.OrganizationId);
            HasRequired(a => a.Application)
                .WithMany(b => b.OrganizationApplications)
                .HasForeignKey(c => c.ApplicationId);
        }
    }
}