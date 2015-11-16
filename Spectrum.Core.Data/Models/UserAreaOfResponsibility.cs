using System;

namespace Spectrum.Core.Data.Models
{
    public class UserAreaOfResponsibility
    {
        public int UserId { get; set; }
        public int AreaOfResponsibilityId { get; set; }
        public bool? Cloaked { get; set; }
        public bool? Archive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedByUserId { get; set; }

        public virtual AreaOfResponsibility AreaOfResponsibility { get; set; }
        public virtual User User { get; set; }
    }
}