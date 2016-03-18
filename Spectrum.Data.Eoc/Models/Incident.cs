using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Spectrum.Data.Eoc.Models
{
    public class Incident
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        public int UserId { get; set; }
        public int OrganizationId { get; set; }
        public string IncidentName { get; set; }
        public string Type { get; set; }
        public int Level { get; set; }
        public string Status { get; set; }
        public DateTime CreateDate { get; set; }
        public IList<Log> Logs { get; set; }
    }
}