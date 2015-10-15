namespace Spectrum.Logic.Models
{
    // UserProfile
    public class UserProfileModel
    {
        public int Id { get; set; } // Id (Primary key)
        public int UserId { get; set; } // UserId
        public int? OrganizationId { get; set; } // OrganizationId
        public bool Default { get; set; } // Default
        public string ProfileName { get; set; } // ProfileName
        public string Title { get; set; } // Title
        public string FirstName { get; set; } // FirstName
        public string MiddleName { get; set; } // MiddleName
        public string LastName { get; set; } // LastName
        public string Nickname { get; set; } // Nickname
        public string SecondaryEmail { get; set; } // SecondaryEmail
        public string SecondaryPhoneNumber { get; set; } // SecondaryPhone
        public string TimeZone { get; set; } // TimeZone
        public bool? DstAdjust { get; set; } // DstAdjust
        public string Language { get; set; } // Language
        public byte[] Photo { get; set; } // Photo
        public string Position { get; set; } // Position

        // Foreign keys
        public virtual OrganizationModel Organization { get; set; } // FK_UserProfile_OrganizationId
        public virtual UserModel User { get; set; } // FK_UserProfile_User
    }

}
