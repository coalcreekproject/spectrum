using System;
using System.Collections.Generic;

namespace Spectrum.Core.Data.Models
{
    public partial class Address
    {
        public Address()
        {
            OrganizationProfiles = new List<OrganizationProfile>();
            UserProfiles = new List<UserProfile>();
            InitializePartial();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool Default { get; set; }
        public string Description { get; set; }
        public string StreetOne { get; set; }
        public string StreetTwo { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public bool? Cloaked { get; set; }
        public bool? Archive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedByUserId { get; set; }

        public virtual ICollection<OrganizationProfile> OrganizationProfiles { get; set; }
        public virtual ICollection<UserProfile> UserProfiles { get; set; }

        partial void InitializePartial();
    }
}