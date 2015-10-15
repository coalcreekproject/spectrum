using System.ComponentModel.DataAnnotations.Schema;
using Spectrum.Core.Data.Models.Interfaces;

namespace Spectrum.Core.Data.Models
{
    // AddressInternational
    public partial class AddressInternational : IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }

}
