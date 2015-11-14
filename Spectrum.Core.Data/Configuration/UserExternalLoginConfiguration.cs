using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Configuration
{
    internal class UserExternalLoginConfiguration : EntityTypeConfiguration<UserExternalLogin>
    {
        public UserExternalLoginConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".UserExternalLogin");
            HasKey(x => new { x.UserId, x.LoginProvider, x.ProviderKey });

            Property(x => x.UserId)
                .HasColumnName("UserId")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.LoginProvider)
                .HasColumnName("LoginProvider")
                .IsRequired()
                .HasMaxLength(128)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.ProviderKey)
                .HasColumnName("ProviderKey")
                .IsRequired()
                .HasMaxLength(128)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Cloaked).HasColumnName("Cloaked").IsOptional();
            Property(x => x.Archive).HasColumnName("Archive").IsOptional();
            Property(x => x.CreatedDate).HasColumnName("CreatedDate").IsOptional();
            Property(x => x.CreatedByUserId).HasColumnName("CreatedByUserId").IsOptional();
            Property(x => x.ModifiedDate).HasColumnName("ModifiedDate").IsOptional();
            Property(x => x.ModifiedByUserId).HasColumnName("ModifiedByUserId").IsOptional();

            HasRequired(a => a.User).WithMany(b => b.UserExternalLogins).HasForeignKey(c => c.UserId);
        }
    }
}