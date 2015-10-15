using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Configuration
{
    // ApplicationProfile
    internal partial class ApplicationProfileConfiguration : EntityTypeConfiguration<ApplicationProfile>
    {
        public ApplicationProfileConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".ApplicationProfile");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.ApplicationId).HasColumnName("ApplicationId").IsRequired();
            Property(x => x.Description).HasColumnName("Description").IsOptional();
            Property(x => x.Company).HasColumnName("Company").IsRequired().HasMaxLength(128);
            Property(x => x.Author).HasColumnName("Author").IsRequired().HasMaxLength(128);
            Property(x => x.License).HasColumnName("License").IsOptional().HasMaxLength(256);

            // Foreign keys
            HasRequired(a => a.Application).WithMany(b => b.ApplicationProfiles).HasForeignKey(c => c.ApplicationId); // FK_ApplicationDetail_Application
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
