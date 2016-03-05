namespace Spectrum.Web.Models
{
    public class UserProfileViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; } // UserId
        public bool Default { get; set; } // DefaultOrganizationId
        public int? OrganizationId { get; set; } // DefaultOrganizationId
        public string ProfileName { get; set; } // ProfileName
        public string Title { get; set; } // Title
        public string FirstName { get; set; } // FirstName
        public string MiddleName { get; set; } // MiddleName
        public string LastName { get; set; } // LastName
        public string Nickname { get; set; } // Nickname
        public string SecondaryEmail { get; set; } // SecondaryEmail
        public string SecondaryPhoneNumber { get; set; }
        public string TimeZone { get; set; } // TimeZone
        public bool? DstAdjust { get; set; } // DstAdjust
        public string Language { get; set; } // Language
    }
}