namespace Spectrum.Core.Data.Models
{
    public partial class UserOrganization
    {
        public int UserId { get; set; }
        public int OrganizationId { get; set; }

        public virtual Organization Organization { get; set; }
        public virtual User User { get; set; }
    }
}