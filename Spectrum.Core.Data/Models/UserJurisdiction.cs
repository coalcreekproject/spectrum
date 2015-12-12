namespace Spectrum.Data.Core.Models
{
    public partial class UserJurisdiction
    {
        public int UserId { get; set; }
        public int JurisdictionId { get; set; }

        public virtual Jurisdiction Jurisdiction { get; set; }
        public virtual User User { get; set; }
    }
}