using System;

namespace Spectrum.Core.Data.Models
{
    public class AreaOfResponsibilityProfile
    {
        public int Id { get; set; }
        public int AreaOfResponsibilityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Cloaked { get; set; }
        public bool? Archive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedByUserId { get; set; }

        public virtual AreaOfResponsibility AreaOfResponsibility { get; set; }
    }
}