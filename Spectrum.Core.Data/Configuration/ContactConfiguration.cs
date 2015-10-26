using Spectrum.Core.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Spectrum.Core.Data.Configuration
{
    // Contact
    internal partial class ContactConfiguration : EntityTypeConfiguration<Contact>
    {
        public ContactConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Contact");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.UserId).HasColumnName("UserId").IsRequired();
            Property(x => x.OrganizationId).HasColumnName("OrganizationId").IsOptional();
            Property(x => x.Title).HasColumnName("Title").IsOptional().HasMaxLength(100);
            Property(x => x.FirstName).HasColumnName("FirstName").IsOptional().HasMaxLength(100);
            Property(x => x.MiddleName).HasColumnName("MiddleName").IsOptional().HasMaxLength(100);
            Property(x => x.LastName).HasColumnName("LastName").IsOptional().HasMaxLength(100);
            Property(x => x.Nickname).HasColumnName("Nickname").IsOptional().HasMaxLength(100);
            Property(x => x.PrimaryEmail).HasColumnName("PrimaryEmail").IsOptional().HasMaxLength(100);
            Property(x => x.PrimaryPhoneNumber).HasColumnName("PrimaryPhoneNumber").IsOptional().HasMaxLength(25);
            Property(x => x.SecondaryEmail).HasColumnName("SecondaryEmail").IsOptional().HasMaxLength(100);
            Property(x => x.SecondaryPhoneNumber).HasColumnName("SecondaryPhoneNumber").IsOptional().HasMaxLength(25);
            Property(x => x.TimeZone).HasColumnName("TimeZone").IsOptional().HasMaxLength(100);
            Property(x => x.Language).HasColumnName("Language").IsOptional().HasMaxLength(100);
            Property(x => x.Note).HasColumnName("Note").IsOptional();
            Property(x => x.Photo).HasColumnName("Photo").IsOptional();
            Property(x => x.Position).HasColumnName("Position").IsOptional().HasMaxLength(100);
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
