﻿using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.UI;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Spectrum.Core.Data.Models;
using Spectrum.Core.Data.Caching;
using Spectrum.Core.Data.Caching.Extensions;
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
            CacheLoggedInUser(user);
            return user.GenerateUserIdentityAsync((ApplicationUserManager) UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options,
            IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }

        public void CacheLoggedInUser(User user)
        {
            //var user = UserManager.FindByEmailAsync();
            RedisCache.Set(user.Id.ToString(), user);
        }
    }
}
