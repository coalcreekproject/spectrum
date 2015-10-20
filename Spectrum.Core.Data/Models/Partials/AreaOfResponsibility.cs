using System;
using System.ComponentModel.DataAnnotations.Schema;
using Spectrum.Core.Data.Models.Interfaces;

namespace Spectrum.Core.Data.Models
{
    // AreaOfResponsibility
    [Serializable]
    public partial class AreaOfResponsibility : IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }

}
