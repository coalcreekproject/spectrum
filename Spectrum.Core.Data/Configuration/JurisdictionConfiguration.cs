using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Configuration
{
    // Jurisdiction
    internal partial class JurisdictionConfiguration : EntityTypeConfiguration<Jurisdiction>
    {
        public JurisdictionConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Jurisdiction");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(128);
            Property(x => x.OrganizationId).HasColumnName("OrganizationId").IsRequired();

            // Foreign keys
            HasRequired(a => a.Organization).WithMany(b => b.Jurisdictions).HasForeignKey(c => c.OrganizationId); // FK_Jurisdiction_Organization
            HasMany(t => t.Users).WithMany(t => t.Jurisdictions).Map(m =>
            {
                m.ToTable("UserJurisdiction", schema);
                m.MapLeftKey("JurisdictionId");
                m.MapRightKey("UserId");
            });
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
