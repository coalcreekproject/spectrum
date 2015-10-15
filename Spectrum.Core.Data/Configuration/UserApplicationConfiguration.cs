using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Configuration
{
    // UserApplication
    internal partial class UserApplicationConfiguration : EntityTypeConfiguration<UserApplication>
    {
        public UserApplicationConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".UserApplication");
            HasKey(x => new { x.UserId, x.ApplicationId });

            Property(x => x.UserId).HasColumnName("UserId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.ApplicationId).HasColumnName("ApplicationId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Key).HasColumnName("Key").IsOptional().HasMaxLength(128);

            // Foreign keys
            HasRequired(a => a.User).WithMany(b => b.UserApplications).HasForeignKey(c => c.UserId); // FK_UserApplication_User
            HasRequired(a => a.Application).WithMany(b => b.UserApplications).HasForeignKey(c => c.ApplicationId); // FK_UserApplication_Application
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
