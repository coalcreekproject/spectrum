using System;

namespace Spectrum.Logic.Models
{
    [Serializable]
    public class JurisdictionNoteModel
    {
        public int Id { get; set; }

        public int JurisdictionId { get; set; }
        public string Note { get; set; }

        public virtual JurisdictionModel JurisdictionModel { get; set; }
    }
}