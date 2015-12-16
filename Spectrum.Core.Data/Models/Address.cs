using System;
using System.Collections.Generic;

namespace Spectrum.Data.Core.Models
{
    public partial class Address
    {
        public Address()
        {
            OrganizationProfileAddresses = new List<OrganizationProfileAddress>();
            UserProfileAddresses = new List<UserProfileAddress>();
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

        public virtual ICollection<OrganizationProfileAddress> OrganizationProfileAddresses { get; set; }
        public virtual ICollection<UserProfileAddress> UserProfileAddresses { get; set; }

        partial void InitializePartial();
    }
}