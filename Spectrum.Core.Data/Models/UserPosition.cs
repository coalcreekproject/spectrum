using System.Data.Entity.Spatial;

namespace Spectrum.Data.Core.Models
{
    public partial class UserPosition
    {
        public int UserId { get; set; }
        public int PositionId { get; set; }
        public int OrganizationId { get; set; }
        public bool? Default { get; set; }

        public virtual Organization Organization { get; set; }
        public virtual Position Position { get; set; }
        public virtual User User { get; set; }
    }
}