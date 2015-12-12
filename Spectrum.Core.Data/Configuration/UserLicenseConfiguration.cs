using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Data.Core.Models;

namespace Spectrum.Data.Core.Configuration
{
    internal class UserLicenseConfiguration : EntityTypeConfiguration<UserLicense>
    {
        public UserLicenseConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".UserLicense");
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