using System;
using System.Collections.Generic;

namespace Spectrum.Core.Data.Models
{
    public partial class Jurisdiction
    {
        public Jurisdiction()
        {
            JurisdictionNotes = new List<JurisdictionNote>();
            JurisdictionProfiles = new List<JurisdictionProfile>();
            UserJurisdictions = new List<UserJurisdiction>();
            InitializePartial();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int OrganizationId { get; set; }
        public bool? Cloaked { get; set; }
        public bool? Archive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedByUserId { get; set; }

        public virtual ICollection<JurisdictionNote> JurisdictionNotes { get; set; }
        public virtual ICollection<JurisdictionProfile> JurisdictionProfiles { get; set; }
        public virtual ICollection<UserJurisdiction> UserJurisdictions { get; set; }

        public virtual Organization Organization { get; set; }

        partial void InitializePartial();
    }
}