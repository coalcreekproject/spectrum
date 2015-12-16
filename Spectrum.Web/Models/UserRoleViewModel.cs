using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Spectrum.Data.Core.Models;

namespace Spectrum.Web.Models
{
    public class UserRoleViewModel
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int OrganizationId { get; set; }

        public virtual Organization Organization { get; set; }
        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}