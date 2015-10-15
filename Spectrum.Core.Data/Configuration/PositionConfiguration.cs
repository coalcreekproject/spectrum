using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Configuration
{
    internal partial class PositionConfiguration : EntityTypeConfiguration<Position>
    {
        public PositionConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Position");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.OrganizationId).HasColumnName("OrganizationId").IsOptional().HasMaxLength(100);
            Property(x => x.Name).HasColumnName("Name").IsOptional().HasMaxLength(100);
            Property(x => x.Description).HasColumnName("Description").IsOptional().HasMaxLength(100);
            Property(x => x.Value).HasColumnName("Value").IsOptional().HasMaxLength(100);
            HasMany(t => t.Users).WithMany(t => t.Positions).Map(m => 
            {
                m.ToTable("UserPosition", schema);
                m.MapLeftKey("PositionId");
                m.MapRightKey("UserId");
            });
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
