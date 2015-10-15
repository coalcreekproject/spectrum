using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Configuration
{
    // Parameter
    internal partial class ParameterConfiguration : EntityTypeConfiguration<Parameter>
    {
        public ParameterConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Parameter");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Key).HasColumnName("Key").IsRequired().HasMaxLength(128);
            Property(x => x.Value).HasColumnName("Value").IsRequired().HasMaxLength(128);
            HasMany(t => t.Rules).WithMany(t => t.Parameters).Map(m => 
            {
                m.ToTable("RuleParameter", schema);
                m.MapLeftKey("ParameterId");
                m.MapRightKey("RuleId");
            });
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
