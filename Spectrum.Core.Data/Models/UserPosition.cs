namespace Spectrum.Data.Core.Models
{
    public partial class UserPosition
    {
        public int UserId { get; set; }
        public int PositionId { get; set; }

        public virtual Position Position { get; set; }
        public virtual User User { get; set; }
    }
}