using System;

namespace Spectrum.Core.Data.Models
{
    public class ApplicationParameter
    {
        public int ApplicationId { get; set; }
        public int ParameterId { get; set; }
        public bool? Cloaked { get; set; }
        public bool? Archive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedByUserId { get; set; }

        public virtual Application Application { get; set; }
        public virtual Parameter Parameter { get; set; }
    }
}