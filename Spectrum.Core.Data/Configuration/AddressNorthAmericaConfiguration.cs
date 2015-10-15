using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Configuration
{
    // AddressNorthAmerica
    internal partial class AddressNorthAmericaConfiguration : EntityTypeConfiguration<AddressNorthAmerica>
    {
        public AddressNorthAmericaConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".AddressNorthAmerica");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(128);
            Property(x => x.Default).HasColumnName("Default").IsRequired();
            Property(x => x.Description).HasColumnName("Description").IsOptional().HasMaxLength(256);
            Property(x => x.StreetOne).HasColumnName("StreetOne").IsRequired().HasMaxLength(256);
            Property(x => x.StreetTwo).HasColumnName("StreetTwo").IsOptional().HasMaxLength(256);
            Property(x => x.City).HasColumnName("City").IsRequired().HasMaxLength(128);
            Property(x => x.State).HasColumnName("State").IsRequired().HasMaxLength(2);
            Property(x => x.Zip).HasColumnName("Zip").IsRequired().HasMaxLength(10);
            HasMany(t => t.Organizations).WithMany(t => t.AddressNorthAmericas).Map(m =>
            {
                m.ToTable("OrganizationAddressNorthAmerica", schema);
                m.MapLeftKey("AddressId");
                m.MapRightKey("OrganizationId");
            });
            HasMany(t => t.Users).WithMany(t => t.AddressNorthAmericas).Map(m =>
            {
                m.ToTable("UserAddressNorthAmerica", schema);
                m.MapLeftKey("AddressId");
                m.MapRightKey("UserId");
            });
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
