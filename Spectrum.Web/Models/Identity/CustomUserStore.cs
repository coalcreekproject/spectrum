using Microsoft.AspNet.Identity.EntityFramework;

namespace Spectrum.Web.Models.Identity
{
    public class CustomUserStore : UserStore<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public CustomUserStore(ApplicationDbContext context) 
            : base(context)
        {
        }
    }
}