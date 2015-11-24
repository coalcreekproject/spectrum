using System;

namespace Spectrum.Logic.Models
{
    public partial class UserApplicationModel
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

        public virtual ApplicationModel ApplicationModel { get; set; }
        public virtual UserModel UserModel { get; set; }
    }
}