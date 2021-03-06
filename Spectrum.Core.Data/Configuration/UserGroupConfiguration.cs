using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Data.Core.Models;

namespace Spectrum.Data.Core.Configuration
{
    internal class UserGroupConfiguration : EntityTypeConfiguration<UserGroup>
    {
        public UserGroupConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".UserGroup");
            HasKey(x => new {x.UserId, x.GroupId});

            Property(x => x.UserId)
                .HasColumnName("UserId")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.GroupId)
                .HasColumnName("GroupId")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            HasRequired(a => a.User).WithMany(b => b.UserGroups).HasForeignKey(c => c.UserId);
            HasRequired(a => a.Group).WithMany(b => b.UserGroups).HasForeignKey(c => c.GroupId);
        }
    }
}