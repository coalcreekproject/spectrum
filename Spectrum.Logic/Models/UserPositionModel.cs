using System;

namespace Spectrum.Logic.Models
{
    [Serializable]
    public class UserPositionModel
    {
        public int UserId { get; set; }
        public int PositionId { get; set; }
        public int OrganizationId { get; set; }
        public bool? Default { get; set; }
        public virtual PositionModel Position { get; set; }
    }
}