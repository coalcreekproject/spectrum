using System;
using System.Collections.Generic;

namespace Spectrum.Logic.Models
{
    [Serializable]
    public class PositionModel
    {
        public PositionModel()
        {
            UserPositions = new List<UserPositionModel>();
        }

        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }

        public virtual OrganizationModel Organization { get; set; }
        public virtual ICollection<UserPositionModel> UserPositions { get; set; }
    }
}