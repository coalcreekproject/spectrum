using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Data.Core.Models;

namespace Spectrum.Data.Core.Configuration
{
    internal class OrganizationNoteConfiguration : EntityTypeConfiguration<OrganizationNote>
    {
        public OrganizationNoteConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".OrganizationNote");
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("Id")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.OrganizationId).HasColumnName("OrganizationId").IsRequired();
            Property(x => x.Note).HasColumnName("Note").IsRequired();

            HasRequired(a => a.Organization).WithMany(b => b.OrganizationNotes).HasForeignKey(c => c.OrganizationId);
        }
    }
}