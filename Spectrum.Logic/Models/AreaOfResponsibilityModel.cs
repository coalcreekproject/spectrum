using System;
using System.Collections.Generic;

namespace Spectrum.Logic.Models
{
    public partial class AreaOfResponsibilityModel
    {
        public AreaOfResponsibilityModel()
        {
            AreaOfResponsibilityNoteModels = new List<AreaOfResponsibilityNoteModel>();
            AreaOfResponsibilityProfileModels = new List<AreaOfResponsibilityProfileModel>();
            UserAreaOfResponsibilitieModels = new List<UserAreaOfResponsibilityModel>();
            InitializePartial();
        }

        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Cloaked { get; set; }
        public bool? Archive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedByUserId { get; set; }

        public virtual ICollection<AreaOfResponsibilityNoteModel> AreaOfResponsibilityNoteModels { get; set; }
        public virtual ICollection<AreaOfResponsibilityProfileModel> AreaOfResponsibilityProfileModels { get; set; }
        public virtual ICollection<UserAreaOfResponsibilityModel> UserAreaOfResponsibilitieModels { get; set; }

        public virtual OrganizationModel Organization { get; set; }

        partial void InitializePartial();
    }
}