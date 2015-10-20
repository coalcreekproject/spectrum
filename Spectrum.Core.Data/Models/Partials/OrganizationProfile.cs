using System;
using System.ComponentModel.DataAnnotations.Schema;
using Spectrum.Core.Data.Models.Interfaces;

namespace Spectrum.Core.Data.Models
{
    // OrganizationProfile
    [Serializable]
    public partial class OrganizationProfile : IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }

}
