using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Configuration
{
    internal class AreaOfResponsibilityProfileConfiguration : EntityTypeConfiguration<AreaOfResponsibilityProfile>
    {
        public AreaOfResponsibilityProfileConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".AreaOfResponsibilityProfile");
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("Id")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.AreaOfResponsibilityId).HasColumnName("AreaOfResponsibilityId").IsRequired();
            Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(128);
            Property(x => x.Description).HasColumnName("Description").IsOptional();
            Property(x => x.Cloaked).HasColumnName("Cloaked").IsOptional();
            Property(x => x.Archive).HasColumnName("Archive").IsOptional();
            Property(x => x.CreatedDate).HasColumnName("CreatedDate").IsOptional();
            Property(x => x.CreatedByUserId).HasColumnName("CreatedByUserId").IsOptional();
            Property(x => x.ModifiedDate).HasColumnName("ModifiedDate").IsOptional();
            Property(x => x.ModifiedByUserId).HasColumnName("ModifiedByUserId").IsOptional();

            HasRequired(a => a.AreaOfResponsibility)
                .WithMany(b => b.AreaOfResponsibilityProfiles)
                .HasForeignKey(c => c.AreaOfResponsibilityId);
        }
    }
}