using System;
using System.ComponentModel.DataAnnotations.Schema;
using Spectrum.Core.Data.Models.Interfaces;

namespace Spectrum.Core.Data.Models
{
    // Rule
    [Serializable]
    public partial class Rule : IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }

}
