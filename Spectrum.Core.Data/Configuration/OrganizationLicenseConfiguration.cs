using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Configuration
{
    internal class OrganizationLicenseConfiguration : EntityTypeConfiguration<OrganizationLicense>
    {
        public OrganizationLicenseConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".OrganizationLicense");
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("Id")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(256);
            Property(x => x.Application).HasColumnName("Application").IsRequired();
            Property(x => x.Key).HasColumnName("Key").IsRequired();
        }
    }
}