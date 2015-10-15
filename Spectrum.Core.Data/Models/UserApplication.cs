namespace Spectrum.Core.Data.Models
{
    // UserApplication
    public partial class UserApplication
    {
        public int UserId { get; set; } // UserId (Primary key)
        public int ApplicationId { get; set; } // ApplicationId (Primary key)
        public string Key { get; set; } // Key

        // Foreign keys
        public virtual Application Application { get; set; } // FK_UserApplication_Application
        public virtual User User { get; set; } // FK_UserApplication_User
    }

}
