using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity;
using Spectrum.Data.Core.Models.Interfaces;

namespace Spectrum.Data.Core.Models
{
    public partial class Role : IRole<int>, IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}