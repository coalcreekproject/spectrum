namespace Spectrum.Data.Core.Models
{
    public partial class UserApplication
    {
        public int UserId { get; set; }
        public int ApplicationId { get; set; }
        public string Key { get; set; }

        public virtual Application Application { get; set; }
        public virtual User User { get; set; }
    }
}