using System;

namespace Spectrum.Data.Core.Models
{
    public partial class Contact
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? OrganizationId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string PreferredName { get; set; }
        public string PrimaryEmail { get; set; }
        public string PrimaryPhoneNumber { get; set; }
        public string SecondaryEmail { get; set; }
        public string SecondaryPhoneNumber { get; set; }
        public string TimeZone { get; set; }
        public string Language { get; set; }
        public string Note { get; set; }
        public byte[] Photo { get; set; }
        public string Position { get; set; }
        public bool? Cloaked { get; set; }
        public bool? Archive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedByUserId { get; set; }
    }
}