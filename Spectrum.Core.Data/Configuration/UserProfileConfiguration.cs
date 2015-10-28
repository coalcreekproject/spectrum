using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Configuration
{
    // UserProfile
    internal partial class UserProfileConfiguration : EntityTypeConfiguration<UserProfile>
    {
        public UserProfileConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".UserProfile");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.UserId).HasColumnName("UserId").IsRequired();
            Property(x => x.OrganizationId).HasColumnName("OrganizationId").IsOptional();
            Property(x => x.Default).HasColumnName("Default").IsOptional();
            Property(x => x.ProfileName).HasColumnName("ProfileName").IsOptional().HasMaxLength(100);
            Property(x => x.Title).HasColumnName("Title").IsOptional().HasMaxLength(100);
            Property(x => x.FirstName).HasColumnName("FirstName").IsOptional().HasMaxLength(100);
            Property(x => x.MiddleName).HasColumnName("MiddleName").IsOptional().HasMaxLength(100);
            Property(x => x.LastName).HasColumnName("LastName").IsOptional().HasMaxLength(100);
            Property(x => x.Nickname).HasColumnName("Nickname").IsOptional().HasMaxLength(100);
            Property(x => x.SecondaryEmail).HasColumnName("SecondaryEmail").IsOptional().HasMaxLength(100);
            Property(x => x.SecondaryPhoneNumber).HasColumnName("SecondaryPhoneNumber").IsOptional().HasMaxLength(25);
            Property(x => x.TimeZone).HasColumnName("TimeZone").IsOptional().HasMaxLength(100);
            Property(x => x.DstAdjust).HasColumnName("DstAdjust").IsOptional();
            Property(x => x.Language).HasColumnName("Language").IsOptional().HasMaxLength(100);
            Property(x => x.Photo).HasColumnName("Photo").IsOptional();
            Property(x => x.Position).HasColumnName("Position").IsOptional().HasMaxLength(100);

            // Foreign keys
            HasRequired(a => a.User).WithMany(b => b.UserProfiles).HasForeignKey(c => c.UserId); // FK_UserProfile_User
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
