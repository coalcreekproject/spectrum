using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Configuration
{
    internal class UserPreferenceConfiguration : EntityTypeConfiguration<UserPreference>
    {
        public UserPreferenceConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".UserPreference");
            HasKey(x => new {x.UserId, x.PreferenceId});

            Property(x => x.UserId)
                .HasColumnName("UserId")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.PreferenceId)
                .HasColumnName("PreferenceId")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            HasRequired(a => a.User).WithMany(b => b.UserPreferences).HasForeignKey(c => c.UserId);
            HasRequired(a => a.Preference).WithMany(b => b.UserPreferences).HasForeignKey(c => c.PreferenceId);
        }
    }
}