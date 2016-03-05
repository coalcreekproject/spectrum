namespace Spectrum.Data.Eoc.Models
{
    public class Incident
    {
        public string Id { get; set; }
        public int UserId { get; set; }
        public int OrganizationId { get; set; }
        public string IncidentName { get; set; }
        public string Type { get; set; }
        public int Level { get; set; }
        public string Status { get; set; }
    }
}