namespace Spectrum.Logic.Models
{
    public class OrganizationNoteModel
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string Note { get; set; }

        public virtual OrganizationModel Organization { get; set; }
    }
}