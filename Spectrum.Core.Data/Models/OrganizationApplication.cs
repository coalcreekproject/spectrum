namespace Spectrum.Data.Core.Models
{
    public partial class OrganizationApplication
    {
        public int OrganizationId { get; set; }
        public int ApplicationId { get; set; }
        public string Key { get; set; }

        public virtual Application Application { get; set; }
        public virtual Organization Organization { get; set; }
    }
}