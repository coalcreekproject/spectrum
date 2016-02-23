namespace Spectrum.Web.Models
{
    public class UserPositionViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public int UserId { get; set; }
        public int PositionId { get; set; }
        public int OrganizationId { get; set; }
        public bool? Default { get; set; }

        //public virtual Organization Organization { get; set; }
        //public virtual PositionViewModel Position { get; set; }
        //public virtual UserViewModel User { get; set; }
    }
}