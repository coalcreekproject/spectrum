using System;
using System.Collections.Generic;

namespace Spectrum.Core.Data.Models
{
    public partial class Position
    {
        public Position()
        {
            UserPositions = new List<UserPosition>();
            InitializePartial();
        }

        public int Id { get; set; }
        public string OrganizationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public bool? Cloaked { get; set; }
        public bool? Archive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedByUserId { get; set; }

        public virtual ICollection<UserPosition> UserPositions { get; set; }

        partial void InitializePartial();
    }
}