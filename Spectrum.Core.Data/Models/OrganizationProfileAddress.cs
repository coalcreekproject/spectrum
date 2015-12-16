namespace Spectrum.Data.Core.Models
{
    public partial class OrganizationProfileAddress
    {
        public int OrganizationProfileId { get; set; }
        public int AddressId { get; set; }

        public virtual Address Address { get; set; }
        public virtual OrganizationProfile OrganizationProfile { get; set; }
    }
}