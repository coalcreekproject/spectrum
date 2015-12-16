using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Data.Core.Models;

namespace Spectrum.Data.Core.Configuration
{
    internal class UserAreaOfResponsibilityConfiguration : EntityTypeConfiguration<UserAreaOfResponsibility>
    {
        public UserAreaOfResponsibilityConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".UserAreaOfResponsibility");
            HasKey(x => new {x.UserId, x.AreaOfResponsibilityId});

            Property(x => x.UserId)
                .HasColumnName("UserId")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.AreaOfResponsibilityId)
                .HasColumnName("AreaOfResponsibilityId")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            HasRequired(a => a.User).WithMany(b => b.UserAreaOfResponsibilities).HasForeignKey(c => c.UserId);
            HasRequired(a => a.AreaOfResponsibility)
                .WithMany(b => b.UserAreaOfResponsibilities)
                .HasForeignKey(c => c.AreaOfResponsibilityId);
        }
    }
}