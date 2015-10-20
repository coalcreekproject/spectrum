using System;
using System.ComponentModel.DataAnnotations.Schema;
using Spectrum.Core.Data.Models.Interfaces;

namespace Spectrum.Core.Data.Models
{
    // Preference
    [Serializable]
    public partial class Preference : IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }

}
