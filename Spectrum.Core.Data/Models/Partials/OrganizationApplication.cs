using System;
using System.ComponentModel.DataAnnotations.Schema;
using Spectrum.Core.Data.Models.Interfaces;

namespace Spectrum.Core.Data.Models
{
    [Serializable]
    public partial class OrganizationApplication : IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }

}
