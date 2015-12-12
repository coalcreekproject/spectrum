using System.Collections.Generic;

namespace Spectrum.Data.Core.Models
{
    public partial class Preference
    {
        public Preference()
        {
            OrganizationPreferences = new List<OrganizationPreference>();
            UserPreferences = new List<UserPreference>();
            InitializePartial();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }

        public virtual ICollection<OrganizationPreference> OrganizationPreferences { get; set; }
        public virtual ICollection<UserPreference> UserPreferences { get; set; }

        partial void InitializePartial();
    }
}