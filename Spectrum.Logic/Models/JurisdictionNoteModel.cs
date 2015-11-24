namespace Spectrum.Logic.Models
{
    public class JurisdictionNoteModel
    {
        public int Id { get; set; }
        public int JurisdictionId { get; set; }
        public string Note { get; set; }

        public virtual JurisdictionModel JurisdictionModel { get; set; }
    }
}