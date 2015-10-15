using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Configuration
{
    // Preference
    internal partial class PreferenceConfiguration : EntityTypeConfiguration<Preference>
    {
        public PreferenceConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Preference");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(128);
            Property(x => x.Description).HasColumnName("Description").IsOptional().HasMaxLength(256);
            Property(x => x.Value).HasColumnName("Value").IsRequired();
            HasMany(t => t.Users).WithMany(t => t.Preferences).Map(m =>
            {
                m.ToTable("UserPreference", schema);
                m.MapLeftKey("PreferenceId");
                m.MapRightKey("UserId");
            });
            HasMany(t => t.Organizations).WithMany(t => t.Preferences).Map(m =>
            {
                m.ToTable("OrganizationPreference", schema);
                m.MapLeftKey("PreferenceId");
                m.MapRightKey("OrganizationId");
            });
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
