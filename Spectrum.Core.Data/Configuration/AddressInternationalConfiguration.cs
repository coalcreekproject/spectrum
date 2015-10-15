using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Configuration
{
    // AddressInternational
    internal partial class AddressInternationalConfiguration : EntityTypeConfiguration<AddressInternational>
    {
        public AddressInternationalConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".AddressInternational");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(128);
            Property(x => x.Default).HasColumnName("Default").IsRequired();
            Property(x => x.Description).HasColumnName("Description").IsOptional().HasMaxLength(256);
            Property(x => x.StreetOne).HasColumnName("StreetOne").IsRequired().HasMaxLength(256);
            Property(x => x.StreetTwo).HasColumnName("StreetTwo").IsOptional().HasMaxLength(256);
            Property(x => x.Country).HasColumnName("Country").IsRequired().HasMaxLength(128);
            Property(x => x.City).HasColumnName("City").IsRequired().HasMaxLength(128);
            Property(x => x.PoliticalBoundary).HasColumnName("PoliticalBoundary").IsRequired().HasMaxLength(128);
            Property(x => x.PostalCode).HasColumnName("Postal Code").IsRequired().HasMaxLength(128);
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
