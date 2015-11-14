using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Configuration
{
    internal class UserClaimConfiguration : EntityTypeConfiguration<UserClaim>
    {
        public UserClaimConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".UserClaim");
            HasKey(x => x.ClaimId);

            Property(x => x.UserId).HasColumnName("UserId").IsRequired();
            Property(x => x.ClaimId)
                .HasColumnName("ClaimId")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.ClaimType).HasColumnName("ClaimType").IsOptional();
            Property(x => x.ClaimValue).HasColumnName("ClaimValue").IsOptional();
            Property(x => x.Cloaked).HasColumnName("Cloaked").IsOptional();
            Property(x => x.Archive).HasColumnName("Archive").IsOptional();
            Property(x => x.CreatedDate).HasColumnName("CreatedDate").IsOptional();
            Property(x => x.CreatedByUserId).HasColumnName("CreatedByUserId").IsOptional();
            Property(x => x.ModifiedDate).HasColumnName("ModifiedDate").IsOptional();
            Property(x => x.ModifiedByUserId).HasColumnName("ModifiedByUserId").IsOptional();

            HasRequired(a => a.User).WithMany(b => b.UserClaims).HasForeignKey(c => c.UserId);
        }
    }
}