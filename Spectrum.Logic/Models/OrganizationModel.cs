using System;

namespace Spectrum.Logic.Models
{
    [Serializable]
    public class OrganizationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? OrganizationTypeId { get; set; }
    }
}