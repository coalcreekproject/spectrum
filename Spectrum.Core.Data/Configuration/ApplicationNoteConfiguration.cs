using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Data.Core.Models;

namespace Spectrum.Data.Core.Configuration
{
    internal class ApplicationNoteConfiguration : EntityTypeConfiguration<ApplicationNote>
    {
        public ApplicationNoteConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".ApplicationNote");
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("Id")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.ApplicationId).HasColumnName("ApplicationId").IsRequired();
            Property(x => x.Note).HasColumnName("Note").IsRequired();

            HasRequired(a => a.Application).WithMany(b => b.ApplicationNotes).HasForeignKey(c => c.ApplicationId);
        }
    }
}