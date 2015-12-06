namespace Spectrum.Core.Data.Models
{
    public partial class OrganizationPreference
    {
        public int OrganizationId { get; set; }
        public int PreferenceId { get; set; }

        public virtual Organization Organization { get; set; }
        public virtual Preference Preference { get; set; }
    }
}