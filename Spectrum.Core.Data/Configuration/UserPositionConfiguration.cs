using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Configuration
{
    internal class UserPositionConfiguration : EntityTypeConfiguration<UserPosition>
    {
        public UserPositionConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".UserPosition");
            HasKey(x => new {x.UserId, x.PositionId});

            Property(x => x.UserId)
                .HasColumnName("UserId")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.PositionId)
                .HasColumnName("PositionId")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            HasRequired(a => a.User).WithMany(b => b.UserPositions).HasForeignKey(c => c.UserId);
            HasRequired(a => a.Position).WithMany(b => b.UserPositions).HasForeignKey(c => c.PositionId);
        }
    }
}