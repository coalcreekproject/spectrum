using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Configuration
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
            Property(x => x.Cloaked).HasColumnName("Cloaked").IsOptional();
            Property(x => x.Archive).HasColumnName("Archive").IsOptional();
            Property(x => x.CreatedDate).HasColumnName("CreatedDate").IsOptional();
            Property(x => x.CreatedByUserId).HasColumnName("CreatedByUserId").IsOptional();
            Property(x => x.ModifiedDate).HasColumnName("ModifiedDate").IsOptional();
            Property(x => x.ModifiedByUserId).HasColumnName("ModifiedByUserId").IsOptional();

            HasRequired(a => a.User).WithMany(b => b.UserAreaOfResponsibilities).HasForeignKey(c => c.UserId);
            HasRequired(a => a.AreaOfResponsibility)
                .WithMany(b => b.UserAreaOfResponsibilities)
                .HasForeignKey(c => c.AreaOfResponsibilityId);
        }
    }
}