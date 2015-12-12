namespace Spectrum.Data.Core.Models
{
    public partial class UserPreference
    {
        public int UserId { get; set; }
        public int PreferenceId { get; set; }

        public virtual Preference Preference { get; set; }
        public virtual User User { get; set; }
    }
}