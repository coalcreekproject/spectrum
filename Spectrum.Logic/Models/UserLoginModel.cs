using System;

namespace Spectrum.Logic.Models
{
    [Serializable]
    public class UserLoginModel
    {
        public int UserId { get; set; } // UserId (Primary key)
        public string LoginProvider { get; set; } // LoginProvider (Primary key)
        public string ProviderKey { get; set; } // ProviderKey (Primary key)

        // Foreign keys
        public virtual UserModel User { get; set; } // FK_UserExternalLogin_User
    }

}
