using System;
using System.ComponentModel.DataAnnotations.Schema;
using Spectrum.Core.Data.Models.Interfaces;

namespace Spectrum.Core.Data.Models
{
    // Group
    [Serializable]
    public partial class Group : IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }

}
