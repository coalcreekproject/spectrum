using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Data.Core.Models;

namespace Spectrum.Data.Core.Configuration
{
    internal class UserProfileAddressConfiguration : EntityTypeConfiguration<UserProfileAddress>
    {
        public UserProfileAddressConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".UserProfileAddress");
            HasKey(x => new {x.UserProfileId, x.AddressId});

            Property(x => x.UserProfileId)
                .HasColumnName("UserProfileId")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.AddressId)
                .HasColumnName("AddressId")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            HasRequired(a => a.UserProfile).WithMany(b => b.UserProfileAddresses).HasForeignKey(c => c.UserProfileId);
            HasRequired(a => a.Address).WithMany(b => b.UserProfileAddresses).HasForeignKey(c => c.AddressId);
        }
    }
}