using System;
using System.Collections.Generic;

namespace Spectrum.Core.Data.Models
{
    public partial class OrganizationProfile
    {
        public OrganizationProfile()
        {
            OrganizationProfileAddresses = new List<OrganizationProfileAddress>();
            InitializePartial();
        }

        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public bool Default { get; set; }
        public string ProfileName { get; set; }
        public string Description { get; set; }
        public string PrimaryContact { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string County { get; set; }
        public string TimeZone { get; set; }
        public bool? DstAdjust { get; set; }
        public string Language { get; set; }
        public string Notes { get; set; }
        public bool? Cloaked { get; set; }
        public bool? Archive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedByUserId { get; set; }

        public virtual ICollection<OrganizationProfileAddress> OrganizationProfileAddresses { get; set; }

        public virtual Organization Organization { get; set; }

        partial void InitializePartial();
    }
}