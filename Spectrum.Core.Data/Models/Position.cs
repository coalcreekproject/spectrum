using System;
using System.Collections.Generic;

namespace Spectrum.Data.Core.Models
{
    public partial class Position
    {
        public Position()
        {
            UserPositions = new List<UserPosition>();
            InitializePartial();
        }

        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }

        public virtual ICollection<UserPosition> UserPositions { get; set; }

        partial void InitializePartial();
    }
}