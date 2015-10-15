using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Configuration
{
    // Group
    internal partial class GroupConfiguration : EntityTypeConfiguration<Group>
    {
        public GroupConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Group");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(128);
            Property(x => x.OrganizationId).HasColumnName("OrganizationId").IsRequired();
            Property(x => x.Description).HasColumnName("Description").IsOptional().HasMaxLength(256);

            // Foreign keys
            HasRequired(a => a.Organization).WithMany(b => b.Groups).HasForeignKey(c => c.OrganizationId); // FK_Group_Organization
            HasMany(t => t.Users).WithMany(t => t.Groups).Map(m => 
            {
                m.ToTable("UserGroup", schema);
                m.MapLeftKey("GroupId");
                m.MapRightKey("UserId");
            });
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
