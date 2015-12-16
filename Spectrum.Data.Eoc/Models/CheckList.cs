using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrum.Data.Eoc.Models
{
    class CheckList
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public List<dynamic> ListData { get; set; }
    }
}
