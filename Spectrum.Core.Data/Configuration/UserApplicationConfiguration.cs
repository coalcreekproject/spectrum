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
            Property(x => x.Key).HasColumnName("Key").IsOptional().HasMaxLength(128);
            Property(x => x.Cloaked).HasColumnName("Cloaked").IsOptional();
            Property(x => x.Archive).HasColumnName("Archive").IsOptional();
            Property(x => x.CreatedDate).HasColumnName("CreatedDate").IsOptional();
            Property(x => x.CreatedByUserId).HasColumnName("CreatedByUserId").IsOptional();
            Property(x => x.ModifiedDate).HasColumnName("ModifiedDate").IsOptional();
            Property(x => x.ModifiedByUserId).HasColumnName("ModifiedByUserId").IsOptional();

            HasRequired(a => a.User).WithMany(b => b.UserApplications).HasForeignKey(c => c.UserId);
            HasRequired(a => a.Application).WithMany(b => b.UserApplications).HasForeignKey(c => c.ApplicationId);
        }
    }
}