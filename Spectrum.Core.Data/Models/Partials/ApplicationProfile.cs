using System.ComponentModel.DataAnnotations.Schema;
using Spectrum.Core.Data.Models.Interfaces;

namespace Spectrum.Core.Data.Models
{
    // ApplicationProfile

    public partial class ApplicationProfile : IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }

}
