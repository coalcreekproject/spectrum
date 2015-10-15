using Microsoft.AspNet.Identity.EntityFramework;

namespace Spectrum.Web.Models
{
    public class CustomRoleStore : RoleStore<CustomRole, int, CustomUserRole>
    {
        public CustomRoleStore(ApplicationDbContext context) 
            : base(context)
        {
        }
    }
}