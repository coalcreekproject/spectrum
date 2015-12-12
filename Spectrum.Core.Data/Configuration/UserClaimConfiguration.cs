using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Data.Core.Models;

namespace Spectrum.Data.Core.Configuration
{
    internal class UserClaimConfiguration : EntityTypeConfiguration<UserClaim>
    {
        public UserClaimConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".UserClaim");
            HasKey(x => x.ClaimId);

            Property(x => x.ClaimId)
                .HasColumnName("ClaimId")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.UserId).HasColumnName("UserId").IsRequired();
            Property(x => x.ClaimType).HasColumnName("ClaimType").IsOptional();
            Property(x => x.ClaimValue).HasColumnName("ClaimValue").IsOptional();

            HasRequired(a => a.User).WithMany(b => b.UserClaims).HasForeignKey(c => c.UserId);
        }
    }
}