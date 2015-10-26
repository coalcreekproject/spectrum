namespace Spectrum.Core.Data.Models
{
    // Contact
    public partial class Contact
    {
        public int Id { get; set; } // Id (Primary key)
        public int UserId { get; set; } // UserId
        public int? OrganizationId { get; set; } // OrganizationId
        public string Title { get; set; } // Title
        public string FirstName { get; set; } // FirstName
        public string MiddleName { get; set; } // MiddleName
        public string LastName { get; set; } // LastName
        public string Nickname { get; set; } // Nickname
        public string PrimaryEmail { get; set; } // PrimaryEmail
        public string PrimaryPhoneNumber { get; set; } // PrimaryPhoneNumber
        public string SecondaryEmail { get; set; } // SecondaryEmail
        public string SecondaryPhoneNumber { get; set; } // SecondaryPhoneNumber
        public string TimeZone { get; set; } // TimeZone
        public string Language { get; set; } // Language
        public string Note { get; set; } // Note
        public byte[] Photo { get; set; } // Photo
        public string Position { get; set; } // Position
    }

}
