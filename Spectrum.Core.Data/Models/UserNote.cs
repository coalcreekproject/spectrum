namespace Spectrum.Data.Core.Models
{
    public partial class UserNote
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Note { get; set; }

        public virtual User User { get; set; }
    }
}