using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Configuration
{
    // JusrisdictionProfile
    internal partial class JusrisdictionProfileConfiguration : EntityTypeConfiguration<JusrisdictionProfile>
    {
        public JusrisdictionProfileConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".JusrisdictionProfile");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.JurisdictionId).HasColumnName("JurisdictionId").IsRequired();
            Property(x => x.Description).HasColumnName("Description").IsOptional();

            // Foreign keys
            HasRequired(a => a.Jurisdiction).WithMany(b => b.JusrisdictionProfiles).HasForeignKey(c => c.JurisdictionId); // FK_JurisdictionProfile_Jurisdiction
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
