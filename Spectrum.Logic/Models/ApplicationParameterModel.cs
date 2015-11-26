using System;

namespace Spectrum.Logic.Models
{
    [Serializable]
    public class ApplicationParameterModel
    {
        public int ApplicationId { get; set; }
        public int ParameterId { get; set; }
        public bool? Cloaked { get; set; }
        public bool? Archive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedByUserId { get; set; }

        public virtual ApplicationModel ApplicationModel { get; set; }
        public virtual ParameterModel ParameterModels { get; set; }
    }
}