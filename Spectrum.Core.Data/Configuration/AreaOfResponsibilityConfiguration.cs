using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Configuration
{
    internal class AreaOfResponsibilityConfiguration : EntityTypeConfiguration<AreaOfResponsibility>
    {
        public AreaOfResponsibilityConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".AreaOfResponsibility");
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("Id")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.OrganizationId).HasColumnName("OrganizationId").IsRequired();
            Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(100);
            Property(x => x.Description).HasColumnName("Description").IsOptional().HasMaxLength(250);
            Property(x => x.Cloaked).HasColumnName("Cloaked").IsOptional();
            Property(x => x.Archive).HasColumnName("Archive").IsOptional();
            Property(x => x.CreatedDate).HasColumnName("CreatedDate").IsOptional();
            Property(x => x.CreatedByUserId).HasColumnName("CreatedByUserId").IsOptional();
            Property(x => x.ModifiedDate).HasColumnName("ModifiedDate").IsOptional();
            Property(x => x.ModifiedByUserId).HasColumnName("ModifiedByUserId").IsOptional();

            HasRequired(a => a.Organization)
                .WithMany(b => b.AreaOfResponsibilities)
                .HasForeignKey(c => c.OrganizationId);
        }
    }
}