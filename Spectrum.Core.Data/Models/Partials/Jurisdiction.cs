using System;
using System.ComponentModel.DataAnnotations.Schema;
using Spectrum.Core.Data.Models.Interfaces;

namespace Spectrum.Core.Data.Models
{
    // Jurisdiction
    [Serializable]
    public partial class Jurisdiction : IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }

}
