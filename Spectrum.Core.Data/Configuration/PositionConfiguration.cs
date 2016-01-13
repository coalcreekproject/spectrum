using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Data.Core.Models;

namespace Spectrum.Data.Core.Configuration
{
    internal class PositionConfiguration : EntityTypeConfiguration<Position>
    {
        public PositionConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Position");
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("Id")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.OrganizationId).HasColumnName("OrganizationId").IsRequired();
            Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(100);
            Property(x => x.Description).HasColumnName("Description").IsOptional().HasMaxLength(100);
            Property(x => x.Value).HasColumnName("Value").IsOptional().HasMaxLength(100);
        }
    }
}