using System;
using System.Collections.Generic;

namespace Spectrum.Logic.Models
{
    [Serializable]
    public class ApplicationModel
    {
        public ApplicationModel()
        {
            ApplicationNoteModels = new List<ApplicationNoteModel>();
            ApplicationParameterModels = new List<ApplicationParameterModel>();
            ApplicationProfileModels = new List<ApplicationProfileModel>();
            OrganizationApplicationModels = new List<OrganizationApplicationModel>();
            RoleModels = new List<RoleModel>();
            UserApplicationModels = new List<UserApplicationModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool? Cloaked { get; set; }
        public bool? Archive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedByUserId { get; set; }

        public virtual ICollection<ApplicationNoteModel> ApplicationNoteModels { get; set; }
        public virtual ICollection<ApplicationParameterModel> ApplicationParameterModels { get; set; }
        public virtual ICollection<ApplicationProfileModel> ApplicationProfileModels { get; set; }
        public virtual ICollection<OrganizationApplicationModel> OrganizationApplicationModels { get; set; }
        public virtual ICollection<RoleModel> RoleModels { get; set; }
        public virtual ICollection<UserApplicationModel> UserApplicationModels { get; set; }
    }
}