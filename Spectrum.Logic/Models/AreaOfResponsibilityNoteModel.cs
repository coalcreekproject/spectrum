using System;

namespace Spectrum.Logic.Models
{
    [Serializable]
    public class AreaOfResponsibilityNoteModel
    {
        public int Id { get; set; }
        public int AreaOfResponsibilityId { get; set; }
        public string Note { get; set; }

        public virtual AreaOfResponsibilityModel AreaOfResponsibilityModel { get; set; }
    }
}