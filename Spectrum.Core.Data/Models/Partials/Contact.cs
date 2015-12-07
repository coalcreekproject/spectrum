using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectrum.Core.Data.Models.Interfaces;

namespace Spectrum.Core.Data.Models
{
    partial class Contact : IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}
