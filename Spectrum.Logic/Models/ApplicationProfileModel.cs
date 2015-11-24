using System;

namespace Spectrum.Logic.Models
{
    public partial class ApplicationProfileModel
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string Description { get; set; }
        public string Company { get; set; }
        public string Author { get; set; }
        public string License { get; set; }
        public bool? Cloaked { get; set; }
        public bool? Archive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedByUserId { get; set; }

        public virtual ApplicationModel Application { get; set; }
    }
}