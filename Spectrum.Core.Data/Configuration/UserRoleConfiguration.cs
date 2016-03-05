using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Data.Core.Models;

namespace Spectrum.Data.Core.Configuration
{
    internal class UserRoleConfiguration : EntityTypeConfiguration<UserRole>
    {
        public UserRoleConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".UserRole");
            HasKey(x => new { x.UserId, x.RoleId });

            Property(x => x.UserId).HasColumnName("UserId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.RoleId).HasColumnName("RoleId").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.OrganizationId).HasColumnName("OrganizationId").IsRequired();
            Property(x => x.Default).HasColumnName("Default").IsOptional();

            HasRequired(a => a.User).WithMany(b => b.UserRoles).HasForeignKey(c => c.UserId);
            HasRequired(a => a.Role).WithMany(b => b.UserRoles).HasForeignKey(c => c.RoleId);
            HasRequired(a => a.Organization).WithMany().WillCascadeOnDelete(false);
        }
    }
}