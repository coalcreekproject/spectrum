using System;

namespace Spectrum.Logic.Models
{
    public class UserAreaOfResponsibilityModel
    {
        public int UserId { get; set; }
        public int AreaOfResponsibilityId { get; set; }
        public bool? Cloaked { get; set; }
        public bool? Archive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedByUserId { get; set; }

        public virtual AreaOfResponsibilityModel AreaOfResponsibilityModel { get; set; }
        public virtual UserModel UserModel { get; set; }
    }
}