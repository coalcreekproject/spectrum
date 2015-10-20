using System;
using System.ComponentModel.DataAnnotations.Schema;
using Spectrum.Core.Data.Models.Interfaces;

namespace Spectrum.Core.Data.Models
{
    // AddressNorthAmerica
    [Serializable]
    public partial class AddressNorthAmerica : IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }

}
