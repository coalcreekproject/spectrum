using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Configuration
{
    // OrganizationProfile
    internal partial class OrganizationProfileConfiguration : EntityTypeConfiguration<OrganizationProfile>
    {
        public OrganizationProfileConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".OrganizationProfile");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.OrganizationId).HasColumnName("OrganizationId").IsRequired();
            Property(x => x.Default).HasColumnName("Default").IsRequired();
            Property(x => x.ProfileName).HasColumnName("ProfileName").IsRequired();
            Property(x => x.Description).HasColumnName("Description").IsOptional();
            Property(x => x.StreetAddressOne).HasColumnName("StreetAddressOne").IsOptional();
            Property(x => x.StreetAddressTwo).HasColumnName("StreetAddressTwo").IsOptional();
            Property(x => x.City).HasColumnName("City").IsOptional();
            Property(x => x.State).HasColumnName("State").IsOptional().HasMaxLength(50);
            Property(x => x.Zip).HasColumnName("Zip").IsOptional().HasMaxLength(15);
            Property(x => x.Phone).HasColumnName("Phone").IsOptional().HasMaxLength(25);
            Property(x => x.Fax).HasColumnName("Fax").IsOptional().HasMaxLength(25);
            Property(x => x.Email).HasColumnName("Email").IsOptional().HasMaxLength(100);
            Property(x => x.Country).HasColumnName("Country").IsOptional().HasMaxLength(256);
            Property(x => x.County).HasColumnName("County").IsOptional().HasMaxLength(256);
            Property(x => x.TimeZone).HasColumnName("TimeZone").IsOptional().HasMaxLength(100);
            Property(x => x.DstAdjust).HasColumnName("DstAdjust").IsOptional();
            Property(x => x.Language).HasColumnName("Language").IsOptional().HasMaxLength(100);

            // Foreign keys
            HasRequired(a => a.Organization).WithMany(b => b.OrganizationProfiles).HasForeignKey(c => c.OrganizationId); // FK_OrganizationProfile_Organization
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
