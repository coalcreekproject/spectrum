using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Data.Core.Models;

namespace Spectrum.Data.Core.Configuration
{
    internal class RoleConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Role");
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("Id")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(128);
            Property(x => x.OrganizationId).HasColumnName("OrganizationId").IsRequired();
            Property(x => x.Description).HasColumnName("Description").IsOptional().HasMaxLength(256);
            Property(x => x.ApplicationId).HasColumnName("ApplicationId").IsOptional();

            //HasRequired(a => a.Organization).WithMany(b => b.Roles).HasForeignKey(c => c.OrganizationId);
            //HasOptional(a => a.Application).WithMany(b => b.Roles).HasForeignKey(c => c.ApplicationId);
        }
    }
}