namespace Spectrum.Web.Models
{
    public class UserOrganizationViewModel
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public int OrganizationId { get; set; }
        public bool? Default { get; set; }
    }
}