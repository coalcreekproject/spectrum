using System;

namespace Spectrum.Core.Data.Models
{
    public partial class OrganizationApplication
    {
        public int OrganizationId { get; set; }
        public int ApplicationId { get; set; }
        public string Key { get; set; }
        public bool? Cloaked { get; set; }
        public bool? Archive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedByUserId { get; set; }

        public virtual Application Application { get; set; }
        public virtual Organization Organization { get; set; }
    }
}