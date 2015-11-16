using System.ComponentModel.DataAnnotations.Schema;
using Spectrum.Core.Data.Models.Interfaces;

namespace Spectrum.Core.Data.Models
{
    // AddressNorthAmerica

    public partial class Address : IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }

}
