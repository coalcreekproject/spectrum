using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Configuration
{
    internal class AddressConfiguration : EntityTypeConfiguration<Address>
    {
        public AddressConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Address");
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("Id")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(128);
            Property(x => x.Default).HasColumnName("Default").IsRequired();
            Property(x => x.Description).HasColumnName("Description").IsOptional().HasMaxLength(256);
            Property(x => x.StreetOne).HasColumnName("StreetOne").IsRequired().HasMaxLength(256);
            Property(x => x.StreetTwo).HasColumnName("StreetTwo").IsOptional().HasMaxLength(256);
            Property(x => x.City).HasColumnName("City").IsRequired().HasMaxLength(128);
            Property(x => x.State).HasColumnName("State").IsRequired().HasMaxLength(2);
            Property(x => x.Zip).HasColumnName("Zip").IsRequired().HasMaxLength(10);
            Property(x => x.Cloaked).HasColumnName("Cloaked").IsOptional();
            Property(x => x.Archive).HasColumnName("Archive").IsOptional();
            Property(x => x.CreatedDate).HasColumnName("CreatedDate").IsOptional();
            Property(x => x.CreatedByUserId).HasColumnName("CreatedByUserId").IsOptional();
            Property(x => x.ModifiedDate).HasColumnName("ModifiedDate").IsOptional();
            Property(x => x.ModifiedByUserId).HasColumnName("ModifiedByUserId").IsOptional();
        }
    }
}