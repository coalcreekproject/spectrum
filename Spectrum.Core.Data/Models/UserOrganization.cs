namespace Spectrum.Data.Core.Models
{
    public partial class UserOrganization
    {
        public int UserId { get; set; }
        public int OrganizationId { get; set; }
        public bool? Default { get; set; }

        public virtual Organization Organization { get; set; }
        public virtual User User { get; set; }
    }
}