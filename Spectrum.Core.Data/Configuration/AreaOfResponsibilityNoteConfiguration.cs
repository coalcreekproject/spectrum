using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Data.Core.Models;

namespace Spectrum.Data.Core.Configuration
{
    internal class AreaOfResponsibilityNoteConfiguration : EntityTypeConfiguration<AreaOfResponsibilityNote>
    {
        public AreaOfResponsibilityNoteConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".AreaOfResponsibilityNote");
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("Id")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.AreaOfResponsibilityId).HasColumnName("AreaOfResponsibilityId").IsRequired();
            Property(x => x.Note).HasColumnName("Note").IsRequired();

            HasRequired(a => a.AreaOfResponsibility)
                .WithMany(b => b.AreaOfResponsibilityNotes)
                .HasForeignKey(c => c.AreaOfResponsibilityId);
        }
    }
}