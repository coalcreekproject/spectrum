namespace Spectrum.Logic.Models
{
    // OrganizationApplications
    public class OrganizationApplicationModel
    {
        public int OrganizationId { get; set; } // OrganizationId (Primary key)
        public int ApplicationId { get; set; } // ApplicationId (Primary key)
        public string Key { get; set; } // Key

        // Foreign keys
        public virtual ApplicationModel Application { get; set; } // FK_OrganizationApplications_Application
        public virtual OrganizationModel Organization { get; set; } // FK_OrganizationApplications_Organization
    }

}
