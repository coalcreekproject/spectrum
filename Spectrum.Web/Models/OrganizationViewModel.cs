namespace Spectrum.Web.Models
{
    public class OrganizationViewModel
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name
        public int? OrganizationTypeId { get; set; } // OrganizationTypeId
    }
}