using Microsoft.AspNet.Identity.EntityFramework;

namespace Spectrum.Web.Models
{
    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public CustomRole() { }
        public CustomRole(string name) { Name = name; }
    }
}