using System;
using System.ComponentModel.DataAnnotations.Schema;
using Spectrum.Core.Data.Models.Interfaces;

namespace Spectrum.Core.Data.Models
{
    // Application
    [Serializable]
    public partial class Application : IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }

}
