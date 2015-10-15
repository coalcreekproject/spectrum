using System.ComponentModel.DataAnnotations.Schema;
using Spectrum.Core.Data.Models.Interfaces;

namespace Spectrum.Core.Data.Models
{
    // UserProfile
    public partial class UserProfile : IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }

}
