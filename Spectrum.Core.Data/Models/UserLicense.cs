namespace Spectrum.Core.Data.Models
{
    public partial class UserLicense
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Application { get; set; }
        public string Key { get; set; }
    }
}