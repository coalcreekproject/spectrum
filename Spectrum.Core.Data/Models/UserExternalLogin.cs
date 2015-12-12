namespace Spectrum.Data.Core.Models
{
    public partial class UserExternalLogin
    {
        public int UserId { get; set; }
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }

        public virtual User User { get; set; }
    }
}