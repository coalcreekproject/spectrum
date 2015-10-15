using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Configuration
{
    // AreaOfResponsibility
    internal partial class AreaOfResponsibilityConfiguration : EntityTypeConfiguration<AreaOfResponsibility>
    {
        public AreaOfResponsibilityConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".AreaOfResponsibility");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.OrganizationId).HasColumnName("OrganizationId").IsRequired();
            Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(100);
            Property(x => x.Description).HasColumnName("Description").IsOptional().HasMaxLength(250);

            // Foreign keys
            HasRequired(a => a.Organization).WithMany(b => b.AreaOfResponsibilities).HasForeignKey(c => c.OrganizationId); // FK_AreaOfResponsibility_OrganizationId
            HasMany(t => t.Users).WithMany(t => t.AreaOfResponsibilities).Map(m => 
            {
                m.ToTable("UserAreaOfResponsibility", schema);
                m.MapLeftKey("AreaOfResponsibilityId");
                m.MapRightKey("UserId");
            });
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
