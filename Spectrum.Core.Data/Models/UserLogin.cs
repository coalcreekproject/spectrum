namespace Spectrum.Core.Data.Models
{
    // UserExternalLogin
    public partial class UserLogin
    {
        public int UserId { get; set; } // UserId (Primary key)
        public string LoginProvider { get; set; } // LoginProvider (Primary key)
        public string ProviderKey { get; set; } // ProviderKey (Primary key)

        // Foreign keys
        public virtual User User { get; set; } // FK_UserExternalLogin_User
    }

}
