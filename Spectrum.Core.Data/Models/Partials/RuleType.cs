using System.ComponentModel.DataAnnotations.Schema;
using Spectrum.Data.Core.Models.Interfaces;

namespace Spectrum.Data.Core.Models
{
    public partial class RuleType : IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}