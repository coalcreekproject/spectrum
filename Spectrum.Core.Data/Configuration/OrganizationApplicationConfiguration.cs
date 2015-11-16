using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Configuration
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
            Property(x => x.Key).HasColumnName("Key").IsOptional().HasMaxLength(256);
            Property(x => x.Cloaked).HasColumnName("Cloaked").IsOptional();
            Property(x => x.Archive).HasColumnName("Archive").IsOptional();
            Property(x => x.CreatedDate).HasColumnName("CreatedDate").IsOptional();
            Property(x => x.CreatedByUserId).HasColumnName("CreatedByUserId").IsOptional();
            Property(x => x.ModifiedDate).HasColumnName("ModifiedDate").IsOptional();
            Property(x => x.ModifiedByUserId).HasColumnName("ModifiedByUserId").IsOptional();

            HasRequired(a => a.Organization)
                .WithMany(b => b.OrganizationApplications)
                .HasForeignKey(c => c.OrganizationId);
            HasRequired(a => a.Application)
                .WithMany(b => b.OrganizationApplications)
                .HasForeignKey(c => c.ApplicationId);
        }
    }
}