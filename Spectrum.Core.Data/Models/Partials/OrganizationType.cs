using System;
using System.ComponentModel.DataAnnotations.Schema;
using Spectrum.Core.Data.Models.Interfaces;

namespace Spectrum.Core.Data.Models
{
    // OrganizationType
    [Serializable]
    public partial class OrganizationType : IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }

}
