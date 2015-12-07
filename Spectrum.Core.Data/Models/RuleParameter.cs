namespace Spectrum.Core.Data.Models
{
    public partial class RuleParameter
    {
        public int? Id { get; set; }
        public int RuleId { get; set; }
        public int ParameterId { get; set; }

        public virtual Parameter Parameter { get; set; }
        public virtual Rule Rule { get; set; }
    }
}