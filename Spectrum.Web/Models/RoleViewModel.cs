namespace Spectrum.Web.Models
{
    public class RoleViewModel
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name
        public int OrganizationId { get; set; } // OrganizationId
        public string Description { get; set; } // Description
        public int? ApplicationId { get; set; } // ApplicationId
    }
}