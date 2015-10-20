using System;
using System.ComponentModel.DataAnnotations.Schema;
using Spectrum.Core.Data.Models.Interfaces;

namespace Spectrum.Core.Data.Models
{
    // UserApplication
    [Serializable]
    public partial class UserApplication : IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }

}
