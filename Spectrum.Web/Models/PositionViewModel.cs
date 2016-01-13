using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Spectrum.Data.Core.Models;

namespace Spectrum.Web.Models
{
    public class PositionViewModel
    {
        public int Id { get; set; }
        public string OrganizationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
    }
}