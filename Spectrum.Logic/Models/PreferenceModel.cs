using System;
using System.Collections.Generic;

namespace Spectrum.Logic.Models
{
    public partial class PreferenceModel
    {
        public PreferenceModel()
        {
            OrganizationModels = new List<OrganizationModel>();
            UserModels = new List<UserModel>();
            InitializePartial();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public bool? Cloaked { get; set; }
        public bool? Archive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedByUserId { get; set; }

        public virtual ICollection<OrganizationModel> OrganizationModels { get; set; }
        public virtual ICollection<UserModel> UserModels { get; set; }

        partial void InitializePartial();
    }
}