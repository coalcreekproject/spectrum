using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Data.Core.Models;

namespace Spectrum.Data.Core.Configuration
{
    internal class UserJurisdictionConfiguration : EntityTypeConfiguration<UserJurisdiction>
    {
        public UserJurisdictionConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".UserJurisdiction");
            HasKey(x => new {x.UserId, x.JurisdictionId});

            Property(x => x.UserId)
                .HasColumnName("UserId")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.JurisdictionId)
                .HasColumnName("JurisdictionId")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            HasRequired(a => a.User).WithMany(b => b.UserJurisdictions).HasForeignKey(c => c.UserId);
            HasRequired(a => a.Jurisdiction).WithMany(b => b.UserJurisdictions).HasForeignKey(c => c.JurisdictionId);
        }
    }
}