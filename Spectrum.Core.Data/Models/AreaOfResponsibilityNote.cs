namespace Spectrum.Data.Core.Models
{
    public partial class AreaOfResponsibilityNote
    {
        public int Id { get; set; }
        public int AreaOfResponsibilityId { get; set; }
        public string Note { get; set; }

        public virtual AreaOfResponsibility AreaOfResponsibility { get; set; }
    }
}