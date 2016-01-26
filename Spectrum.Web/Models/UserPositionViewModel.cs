namespace Spectrum.Web.Models
{
    public class UserPositionViewModel
    {
        public int UserId { get; set; }
        public int PositionId { get; set; }

        public virtual PositionViewModel Position { get; set; }
        public virtual UserViewModel User { get; set; }
    }
}