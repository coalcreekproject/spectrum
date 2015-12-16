using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Data.Core.Models;

namespace Spectrum.Data.Core.Configuration
{
    internal class JurisdictionNoteConfiguration : EntityTypeConfiguration<JurisdictionNote>
    {
        public JurisdictionNoteConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".JurisdictionNote");
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("Id")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.JurisdictionId).HasColumnName("JurisdictionId").IsRequired();
            Property(x => x.Note).HasColumnName("Note").IsRequired();

            HasRequired(a => a.Jurisdiction).WithMany(b => b.JurisdictionNotes).HasForeignKey(c => c.JurisdictionId);
        }
    }
}