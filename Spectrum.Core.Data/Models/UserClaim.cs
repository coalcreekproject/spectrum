namespace Spectrum.Data.Core.Models
{
    public partial class UserClaim
    {
        public int ClaimId { get; set; }
        public int UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        public virtual User User { get; set; }
    }
}