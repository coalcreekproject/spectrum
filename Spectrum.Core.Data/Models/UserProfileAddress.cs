namespace Spectrum.Core.Data.Models
{
    public partial class UserProfileAddress
    {
        public int UserProfileId { get; set; }
        public int AddressId { get; set; }

        public virtual Address Address { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}