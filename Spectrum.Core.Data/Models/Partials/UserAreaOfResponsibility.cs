using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Spectrum.Core.Data.Models.Interfaces;

namespace Spectrum.Core.Data.Models
{
    public partial class UserAreaOfResponsibility : IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}