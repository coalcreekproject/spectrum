using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Configuration
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
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.JurisdictionId).HasColumnName("JurisdictionId").IsRequired();
            Property(x => x.Note).HasColumnName("Note").IsRequired();

            HasRequired(a => a.Jurisdiction).WithMany(b => b.JurisdictionNotes).HasForeignKey(c => c.JurisdictionId);
        }
    }
}