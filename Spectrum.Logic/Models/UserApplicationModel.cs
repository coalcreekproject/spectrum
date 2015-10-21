using System;

namespace Spectrum.Logic.Models
{
    [Serializable]
    public class UserApplicationModel
    {
        public int UserId { get; set; } // UserId (Primary key)
        public int ApplicationId { get; set; } // ApplicationId (Primary key)
        public string Key { get; set; } // Key

        // Foreign keys
        public virtual ApplicationModel Application { get; set; } // FK_UserApplication_Application
        public virtual UserModel User { get; set; } // FK_UserApplication_User
    }

}
