using System.Collections.Generic;

namespace Spectrum.Data.Core.Models
{
    public partial class Role
    {
        public Role()
        {
            UserRoles = new List<UserRole>();
            InitializePartial();
        }

        public int OrganizationId { get; set; }
        public string Description { get; set; }
        public int? ApplicationId { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }

        public virtual Application Application { get; set; }
        public virtual Organization Organization { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

        partial void InitializePartial();
    }
}