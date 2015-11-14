using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Configuration
{
    internal class JurisdictionConfiguration : EntityTypeConfiguration<Jurisdiction>
    {
        public JurisdictionConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Jurisdiction");
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("Id")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(128);
            Property(x => x.OrganizationId).HasColumnName("OrganizationId").IsRequired();
            Property(x => x.Cloaked).HasColumnName("Cloaked").IsOptional();
            Property(x => x.Archive).HasColumnName("Archive").IsOptional();
            Property(x => x.CreatedDate).HasColumnName("CreatedDate").IsOptional();
            Property(x => x.CreatedByUserId).HasColumnName("CreatedByUserId").IsOptional();
            Property(x => x.ModifiedDate).HasColumnName("ModifiedDate").IsOptional();
            Property(x => x.ModifiedByUserId).HasColumnName("ModifiedByUserId").IsOptional();

            HasRequired(a => a.Organization).WithMany(b => b.Jurisdictions).HasForeignKey(c => c.OrganizationId);
            HasMany(t => t.Users).WithMany(t => t.Jurisdictions).Map(m =>
            {
                m.ToTable("UserJurisdiction", schema);
                m.MapLeftKey("JurisdictionId");
                m.MapRightKey("UserId");
            });
        }
    }
}