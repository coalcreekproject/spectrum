using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Spectrum.Data.Eoc.Models;

namespace Spectrum.Web.Models
{
    public class IncidentEventInputViewModel
    {
        public string Id { get; set; }
        public Event Event { get; set; }
    }
}