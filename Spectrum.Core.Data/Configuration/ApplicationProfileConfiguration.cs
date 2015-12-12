using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Data.Core.Models;

namespace Spectrum.Data.Core.Configuration
{
    internal class ApplicationProfileConfiguration : EntityTypeConfiguration<ApplicationProfile>
    {
        public ApplicationProfileConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".ApplicationProfile");
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("Id")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.ApplicationId).HasColumnName("ApplicationId").IsRequired();
            Property(x => x.Description).HasColumnName("Description").IsOptional().HasMaxLength(256);
            Property(x => x.Company).HasColumnName("Company").IsRequired().HasMaxLength(128);
            Property(x => x.Author).HasColumnName("Author").IsRequired().HasMaxLength(128);
            Property(x => x.License).HasColumnName("License").IsOptional().HasMaxLength(256);
            Property(x => x.Cloaked).HasColumnName("Cloaked").IsOptional();
            Property(x => x.Archive).HasColumnName("Archive").IsOptional();
            Property(x => x.CreatedDate).HasColumnName("CreatedDate").IsOptional();
            Property(x => x.CreatedByUserId).HasColumnName("CreatedByUserId").IsOptional();
            Property(x => x.ModifiedDate).HasColumnName("ModifiedDate").IsOptional();
            Property(x => x.ModifiedByUserId).HasColumnName("ModifiedByUserId").IsOptional();

            HasRequired(a => a.Application).WithMany(b => b.ApplicationProfiles).HasForeignKey(c => c.ApplicationId);
        }
    }
}