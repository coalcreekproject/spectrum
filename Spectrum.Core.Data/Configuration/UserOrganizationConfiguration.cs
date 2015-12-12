using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Data.Core.Models;

namespace Spectrum.Data.Core.Configuration
{
    internal class UserOrganizationConfiguration : EntityTypeConfiguration<UserOrganization>
    {
        public UserOrganizationConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".UserOrganization");
            HasKey(x => new {x.UserId, x.OrganizationId});

            Property(x => x.UserId)
                .HasColumnName("UserId")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.OrganizationId)
                .HasColumnName("OrganizationId")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            HasRequired(a => a.User).WithMany(b => b.UserOrganizations).HasForeignKey(c => c.UserId);
            HasRequired(a => a.Organization).WithMany(b => b.UserOrganizations).HasForeignKey(c => c.OrganizationId);
        }
    }
}