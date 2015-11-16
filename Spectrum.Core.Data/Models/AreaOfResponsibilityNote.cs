namespace Spectrum.Core.Data.Models
{
    public class AreaOfResponsibilityNote
    {
        public int Id { get; set; }
        public int AreaOfResponsibilityId { get; set; }
        public string Note { get; set; }

        public virtual AreaOfResponsibility AreaOfResponsibility { get; set; }
    }
}