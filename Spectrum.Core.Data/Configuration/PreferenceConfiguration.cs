using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Configuration
{
    internal class PreferenceConfiguration : EntityTypeConfiguration<Preference>
    {
        public PreferenceConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Preference");
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("Id")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(128);
            Property(x => x.Description).HasColumnName("Description").IsOptional().HasMaxLength(256);
            Property(x => x.Value).HasColumnName("Value").IsRequired();
            Property(x => x.Cloaked).HasColumnName("Cloaked").IsOptional();
            Property(x => x.Archive).HasColumnName("Archive").IsOptional();
            Property(x => x.CreatedDate).HasColumnName("CreatedDate").IsOptional();
            Property(x => x.CreatedByUserId).HasColumnName("CreatedByUserId").IsOptional();
            Property(x => x.ModifiedDate).HasColumnName("ModifiedDate").IsOptional();
            Property(x => x.ModifiedByUserId).HasColumnName("ModifiedByUserId").IsOptional();
            HasMany(t => t.Users).WithMany(t => t.Preferences).Map(m =>
            {
                m.ToTable("UserPreference", schema);
                m.MapLeftKey("PreferenceId");
                m.MapRightKey("UserId");
            });
        }
    }
}