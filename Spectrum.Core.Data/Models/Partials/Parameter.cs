using System.ComponentModel.DataAnnotations.Schema;
using Spectrum.Core.Data.Models.Interfaces;

namespace Spectrum.Core.Data.Models
{
    // Parameter

    public partial class Parameter : IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }

}
