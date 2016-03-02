using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Logic.Models
{
    [Serializable]
    public class UserOrganizationModel
    {
        public int UserId { get; set; }
        public int OrganizationId { get; set; }
        public bool? Default { get; set; }
        public virtual OrganizationModel Organization { get; set; }
    }
}
