using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity;
using Spectrum.Core.Data.Models.Interfaces;

namespace Spectrum.Core.Data.Models
{
    public partial class Role : IRole<int>, IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}
