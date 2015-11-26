using System;

namespace Spectrum.Logic.Models
{
    [Serializable]
    public class OrganizationNoteModel
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string Note { get; set; }

        public virtual OrganizationModel Organization { get; set; }
    }
}