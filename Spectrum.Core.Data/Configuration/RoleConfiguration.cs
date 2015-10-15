using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Configuration
{
    // Role
    internal partial class RoleConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Role");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(128);
            Property(x => x.OrganizationId).HasColumnName("OrganizationId").IsRequired();
            Property(x => x.Description).HasColumnName("Description").IsOptional().HasMaxLength(256);
            Property(x => x.ApplicationId).HasColumnName("ApplicationId").IsOptional();

            // Foreign keys
            HasRequired(a => a.Organization).WithMany(b => b.Roles).HasForeignKey(c => c.OrganizationId); // FK_Role_Organization
            HasOptional(a => a.Application).WithMany(b => b.Roles).HasForeignKey(c => c.ApplicationId); // FK_Role_Application
            HasMany(t => t.Users).WithMany(t => t.Roles).Map(m => 
            {
                m.ToTable("UserRole", schema);
                m.MapLeftKey("RoleId");
                m.MapRightKey("UserId");
            });
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
