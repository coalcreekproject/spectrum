using System;
using System.Collections.Generic;

namespace Spectrum.Core.Data.Models
{
    public partial class UserProfile
    {
        public UserProfile()
        {
            Addresses = new List<Address>();
            InitializePartial();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int? OrganizationId { get; set; }
        public bool? Default { get; set; }
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
        public bool? Cloaked { get; set; }
        public bool? Archive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedByUserId { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }

        public virtual User User { get; set; }

        partial void InitializePartial();
    }
}