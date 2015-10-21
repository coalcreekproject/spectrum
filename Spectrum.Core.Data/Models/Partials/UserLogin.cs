using System;
using System.ComponentModel.DataAnnotations.Schema;
using Spectrum.Core.Data.Models.Interfaces;

namespace Spectrum.Core.Data.Models
{
    // UserExternalLogin

    public partial class UserLogin : IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }

}
