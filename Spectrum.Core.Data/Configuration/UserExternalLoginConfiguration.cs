using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Configuration
{
    // UserExternalLogin
    internal partial class UserExternalLoginConfiguration : EntityTypeConfiguration<UserLogin>
    {
        public UserExternalLoginConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".UserExternalLogin");
            HasKey(x => new { x.UserId, x.LoginProvider, x.ProviderKey });

            Property(x => x.UserId).HasColumnName("UserId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.LoginProvider).HasColumnName("LoginProvider").IsRequired().HasMaxLength(128).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.ProviderKey).HasColumnName("ProviderKey").IsRequired().HasMaxLength(128).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Foreign keys
            HasRequired(a => a.User).WithMany(b => b.UserExternalLogins).HasForeignKey(c => c.UserId); // FK_UserExternalLogin_User
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
