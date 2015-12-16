using System;
using System.Collections.Generic;

namespace Spectrum.Web.Models
{
    public class RoleViewModel
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ApplicationId { get; set; }
    }
}