using System;

namespace Spectrum.Logic.Models
{
    [Serializable]
    public class UserProfileModel
    {
        public int Id { get; set; }
        public int UserId { get; set; } // UserId
        public int? OrganizationId { get; set; }
        public bool Default { get; set; }
        public string ProfileName { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public string SecondaryEmail { get; set; }
        public string SecondaryPhoneNumber { get; set; }
        public string TimeZone { get; set; }
        public bool? DstAdjust { get; set; }
        public string Language { get; set; }
        public byte[] Photo { get; set; }
        public string Position { get; set; }

        // Foreign keys
        public virtual OrganizationModel Organization { get; set; }
        public virtual UserModel User { get; set; }
    }

}
