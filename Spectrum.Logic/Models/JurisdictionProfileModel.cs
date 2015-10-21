using System;

namespace Spectrum.Logic.Models
{
    [Serializable]
    public class JusrisdictionProfile
    {
        public int Id { get; set; }
        public int JurisdictionId { get; set; }
        public string Description { get; set; }

        // Foreign keys
        public virtual JurisdictionModel Jurisdiction { get; set; }
    }
}