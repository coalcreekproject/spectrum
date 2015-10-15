using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Configuration
{
    // Organization
    internal partial class OrganizationConfiguration : EntityTypeConfiguration<Organization>
    {
        public OrganizationConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Organization");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(128);
            Property(x => x.OrganizationTypeId).HasColumnName("OrganizationTypeId").IsOptional();

            // Foreign keys
            HasOptional(a => a.OrganizationType).WithMany(b => b.Organizations).HasForeignKey(c => c.OrganizationTypeId); // FK_Organization_OrganizationType
            HasMany(t => t.Preferences).WithMany(t => t.Organizations).Map(m =>
            {
                m.ToTable("OrganizationPreference", schema);
                m.MapLeftKey("OrganizationId");
                m.MapRightKey("PreferenceId");
            });
            HasMany(t => t.Users).WithMany(t => t.Organizations).Map(m =>
            {
                m.ToTable("UserOrganization", schema);
                m.MapLeftKey("OrganizationId");
                m.MapRightKey("UserId");
            });
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
