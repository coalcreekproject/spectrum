using System;
using System.Linq;
using System.Runtime.Caching;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.UI;
using AutoMapper;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Spectrum.Data.Core.Caching;
using Spectrum.Data.Core.Caching.Extensions;
using Spectrum.Data.Core.Models;
using Spectrum.Logic.Models;
using StackExchange.Redis;

namespace Spectrum.Web.IdentityConfig
{
    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<User, int>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            MemoryCacheLoggedInUser(user);
            return user.GenerateUserIdentityAsync((ApplicationUserManager) UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options,
            IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }

        public void RedisCacheLoggedInUser(User user)
        {
            var userModel = Mapper.Map<UserModel>(user);

            var cache = RedisCache.Connection.GetDatabase();
            cache.Set("user:" + userModel.Id, userModel);
        }

        public void MemoryCacheLoggedInUser(User user)
        {
            var userModel = Mapper.Map<UserModel>(user);

            //TODO set the default identity focus, do this expensive database operation once and cache it
            //Set the selected organization to the default Organization ID

            var profile = user.UserProfiles.FirstOrDefault(p => p.Default == true);
            var userOrganization = user.UserOrganizations.FirstOrDefault(o => o.OrganizationId == profile.OrganizationId);
            //var organization = userOrganization.Organization;

            //userModel.SelectedOrganizationId = organization.Id;

            //Set the default role for the user
            // do I need to?

            //Set the default position for the user.

            var cache = MemoryCache.Default;
            cache.Set("user:" + userModel.Id, userModel, DateTimeOffset.Now.AddMinutes(30));

            // Yah I know unit tests...
            //var memoryCacheService = new MemoryCacheService();
            //var cacheUserModel = memoryCacheService.GetFromCache<UserModel>("user:" + userModel.Id, null);
            //cacheUserModel.TwoFactorEnabled = true;
        }
    }
}
