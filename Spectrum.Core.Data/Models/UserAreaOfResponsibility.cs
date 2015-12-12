namespace Spectrum.Data.Core.Models
{
    public partial class UserAreaOfResponsibility
    {
        public int UserId { get; set; }
        public int AreaOfResponsibilityId { get; set; }

        public virtual AreaOfResponsibility AreaOfResponsibility { get; set; }
        public virtual User User { get; set; }
    }
}