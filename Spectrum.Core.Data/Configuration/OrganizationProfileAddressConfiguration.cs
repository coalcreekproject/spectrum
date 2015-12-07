using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Configuration
{
    internal class OrganizationProfileAddressConfiguration : EntityTypeConfiguration<OrganizationProfileAddress>
    {
        public OrganizationProfileAddressConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".OrganizationProfileAddress");
            HasKey(x => new {x.OrganizationProfileId, x.AddressId});

            Property(x => x.OrganizationProfileId)
                .HasColumnName("OrganizationProfileId")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.AddressId)
                .HasColumnName("AddressId")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            HasRequired(a => a.OrganizationProfile)
                .WithMany(b => b.OrganizationProfileAddresses)
                .HasForeignKey(c => c.OrganizationProfileId);
            HasRequired(a => a.Address).WithMany(b => b.OrganizationProfileAddresses).HasForeignKey(c => c.AddressId);
        }
    }
}