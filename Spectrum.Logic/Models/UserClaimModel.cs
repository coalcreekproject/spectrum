namespace Spectrum.Logic.Models
{
    // UserClaim
    public class UserClaimModel
    {
        public int UserId { get; set; } // UserId
        public int ClaimId { get; set; } // ClaimId (Primary key)
        public string ClaimType { get; set; } // ClaimType
        public string ClaimValue { get; set; } // ClaimValue

        // Foreign keys
        public virtual UserModel User { get; set; } // FK_UserClaim_User
    }

}
