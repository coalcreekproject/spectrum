using System.ComponentModel.DataAnnotations.Schema;
using Spectrum.Data.Core.Models.Interfaces;

namespace Spectrum.Data.Core.Models
{
    partial class Contact : IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}
