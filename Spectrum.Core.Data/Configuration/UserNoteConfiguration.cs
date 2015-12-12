using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Data.Core.Models;

namespace Spectrum.Data.Core.Configuration
{
    internal class UserNoteConfiguration : EntityTypeConfiguration<UserNote>
    {
        public UserNoteConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".UserNote");
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasColumnName("Id")
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.UserId).HasColumnName("UserId").IsRequired();
            Property(x => x.Note).HasColumnName("Note").IsRequired();

            HasRequired(a => a.User).WithMany(b => b.UserNotes).HasForeignKey(c => c.UserId);
        }
    }
}