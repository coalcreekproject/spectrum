using System;

namespace Spectrum.Core.Data.Models
{
    public partial class UserApplication
    {
        public int UserId { get; set; }
        public int ApplicationId { get; set; }
        public string Key { get; set; }
        public bool? Cloaked { get; set; }
        public bool? Archive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedByUserId { get; set; }

        public virtual Application Application { get; set; }
        public virtual User User { get; set; }
    }
}