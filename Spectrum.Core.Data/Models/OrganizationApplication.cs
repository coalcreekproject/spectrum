namespace Spectrum.Core.Data.Models
{
    // OrganizationApplications
    public partial class OrganizationApplication
    {
        public int OrganizationId { get; set; } // OrganizationId (Primary key)
        public int ApplicationId { get; set; } // ApplicationId (Primary key)
        public string Key { get; set; } // Key

        // Foreign keys
        public virtual Application Application { get; set; } // FK_OrganizationApplications_Application
        public virtual Organization Organization { get; set; } // FK_OrganizationApplications_Organization
    }

}
