using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Configuration
{
    internal class UserApplicationConfiguration : EntityTypeConfiguration<UserApplication>
    {
        public UserApplicationConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".UserApplication");
            HasKey(x => new {x.UserId, x.ApplicationId});

            Property(x => x.UserId)
                .HasColumnName("UserId")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.ApplicationId)
                .HasColumnName("ApplicationId")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Key).HasColumnName("Key").IsRequired().HasMaxLength(128);

            HasRequired(a => a.User).WithMany(b => b.UserApplications).HasForeignKey(c => c.UserId);
            HasRequired(a => a.Application).WithMany(b => b.UserApplications).HasForeignKey(c => c.ApplicationId);
        }
    }
}