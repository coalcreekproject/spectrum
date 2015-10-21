using Microsoft.AspNet.Identity.EntityFramework;

namespace Spectrum.Web.Models.Identity
{
    public class CustomRoleStore : RoleStore<CustomRole, int, CustomUserRole>
    {
        public CustomRoleStore(ApplicationDbContext context) 
            : base(context)
        {
        }
    }
}