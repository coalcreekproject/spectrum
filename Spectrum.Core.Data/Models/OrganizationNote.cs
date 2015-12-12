namespace Spectrum.Data.Core.Models
{
    public partial class OrganizationNote
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string Note { get; set; }

        public virtual Organization Organization { get; set; }
    }
}