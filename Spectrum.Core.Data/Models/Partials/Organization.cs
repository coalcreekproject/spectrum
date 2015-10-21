using System;
using System.ComponentModel.DataAnnotations.Schema;
using Spectrum.Core.Data.Models.Interfaces;

namespace Spectrum.Core.Data.Models
{
    // Organization

    public partial class Organization : IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }

}
