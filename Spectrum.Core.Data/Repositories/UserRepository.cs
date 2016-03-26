using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Spectrum.Data.Core.Context;
using Spectrum.Data.Core.Context.UnitOfWork;
using Spectrum.Data.Core.Models;
using Spectrum.Data.Core.Models.Interfaces;
using Spectrum.Data.Core.Repositories.Interfaces;

namespace Spectrum.Data.Core.Repositories
{
    public class UserRepository : IUserRepository,
        IUserPasswordStore<User, int>,
        IUserLoginStore<User, int>,
        IUserEmailStore<User, int>,
        IUserClaimStore<User, int>,
        IUserRoleStore<User, int>,
        IUserLockoutStore<User, int>,
        IUserTwoFactorStore<User, int>,
        IUserPhoneNumberStore<User, int>
    {
        public UserRepository(ICoreUnitOfWork uow)
        {
            UnitOfWork = uow;
            Context = uow.Context;
        }

        public CoreDbContext Context { get; }

        public ICoreUnitOfWork UnitOfWork { get; }

        public IQueryable<User> Users
        {
            get { return Context.Users; }
        }

        #region IUSerStore Implementation

        public Task CreateAsync(User user)
        {
            Context.Users.Add(user);
            return UnitOfWork.SaveAsync();
        }

        public Task UpdateAsync(User user)
        {
            user.ObjectState = ObjectState.Modified;
            Context.Entry(user).State = EntityState.Modified;
            return UnitOfWork.SaveAsync();
        }

        public Task DeleteAsync(User user)
        {
            Context.Users.Remove(user);
            return UnitOfWork.SaveAsync();
        }

        public Task<User> FindByIdAsync(int userId)
        {
            return Users.FirstOrDefaultAsync(u => u.Id.Equals(userId));
        }

        public Task<User> FindByNameAsync(string userName)
        {
            return Users.FirstOrDefaultAsync(u => u.UserName.Equals(userName));
        }

        #endregion

        #region IUserPasswordStore Implementation

        public Task SetPasswordHashAsync(User user, string password)
        {
            return Task.FromResult(user.PasswordHash = password);
        }

        public Task<string> GetPasswordHashAsync(User user)
        {
            return Task.FromResult(Context.Users.Find(user.Id).PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            var password = Context.Users.Find(user.Id).PasswordHash;
            return Task.FromResult(!string.IsNullOrEmpty(password));
        }

        #endregion

        #region IUserLoginStore Implementation

        public Task AddLoginAsync(User user, UserLoginInfo login)
        {
            var externalLogin = new UserExternalLogin
            {
                LoginProvider = login.LoginProvider,
                ProviderKey = login.ProviderKey
            };

            if (!user.UserExternalLogins.Any(x => x.LoginProvider == login.LoginProvider
                                                  && x.ProviderKey == login.ProviderKey))
            {
                user.UserExternalLogins.Add(externalLogin);
            }

            return Task.FromResult(true);
        }

        public Task<User> FindAsync(UserLoginInfo login)
        {
            return Task.FromResult(Context.Users.Find(login.ProviderKey));
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(User user)
        {
            IList<UserLoginInfo> listLoginInfo =
                user.UserExternalLogins.Select(el => new UserLoginInfo(el.LoginProvider, el.ProviderKey)).ToList();

            return Task.FromResult(listLoginInfo);
        }

        public Task RemoveLoginAsync(User user, UserLoginInfo login)
        {
            var externalLogin = new UserExternalLogin
            {
                LoginProvider = login.LoginProvider,
                ProviderKey = login.ProviderKey
            };

            user.UserExternalLogins.Remove(externalLogin);

            return Task.FromResult(0);
        }

        #endregion

        #region IUserEmailStore Implementation

        public Task SetEmailAsync(User user, string email)
        {
            Context.Users.Find(user).Email = email;
            return UnitOfWork.SaveAsync();
        }

        //TODO: This is not working with the ASP.NET Identity provider when 
        public Task<string> GetEmailAsync(User user)
        {
            var result = Users.FirstOrDefault(u => u.Email.Equals(user.Email));

            if (result == null)
                return Task.FromResult(user.Email);

            return Task.FromResult(result.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(User user)
        {
            return Task.FromResult(Context.Users.Find(user).EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed)
        {
            Context.Users.Find(user).EmailConfirmed = confirmed;
            return UnitOfWork.SaveAsync();
        }

        public Task<User> FindByEmailAsync(string email)
        {
            return Users.FirstOrDefaultAsync(u => u.Email.Equals(email));
        }

        #endregion

        #region IUserClaimStore Implementation

        public Task<IList<Claim>> GetClaimsAsync(User user)
        {
            IList<Claim> result = user.UserClaims.Select(c => new Claim(c.ClaimType, c.ClaimValue)).ToList();
            return Task.FromResult(result);
        }

        public Task AddClaimAsync(User user, Claim claim)
        {
            if (!user.UserClaims.Any(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value))
            {
                user.UserClaims.Add(new UserClaim
                {
                    ClaimType = claim.Type,
                    ClaimValue = claim.Value
                });
            }

            return Task.FromResult(0);
        }

        public Task RemoveClaimAsync(User user, Claim claim)
        {
            var localClaim =
                user.UserClaims.FirstOrDefault(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value);
            return Task.FromResult(user.UserClaims.Remove(localClaim));
        }

        #endregion

        #region IUserRoleStore Implementation

        //Problem, how do we persist a role for a given organization?
        public Task AddToRoleAsync(User user, string roleName)
        {
            //var userRole = new UserRole {Name = roleName};

            var userRole = new UserRole();

            if (!user.UserRoles.Contains(userRole))
            {
                user.UserRoles.Add(userRole);
            }

            return Task.FromResult(true);
        }

        public Task RemoveFromRoleAsync(User user, string roleName)
        {
            var userRoles = user.UserRoles;

            var role = userRoles.Select(r => new UserRole()).FirstOrDefault();

            userRoles.Remove(role);
            return UnitOfWork.SaveAsync();
        }

        public Task<IList<string>> GetRolesAsync(User user)
        {
            var userRoles = user.UserRoles;

            IList<string> results = userRoles.Select(userRole => userRole.RoleId.ToString()).ToList();

            return Task.FromResult(results);
        }

        public Task<bool> IsInRoleAsync(User user, string roleName)
        {
            var userRoles = user.UserRoles;
            IList<string> results = userRoles.Select(userRole => userRole.RoleId.ToString()).ToList();

            return Task.FromResult(results.Count > 0);
        }

        #endregion

        #region IUserLockoutStore Implementation

        public Task<DateTimeOffset> GetLockoutEndDateAsync(User user)
        {
            var result = Context.Users.Find(user.Id);

            if (result.LockoutEndDateUtc == null)
                return Task.FromResult(DateTimeOffset.UtcNow.AddHours(-1));

            var offset = new DateTimeOffset((DateTime) result.LockoutEndDateUtc);

            return Task.FromResult(offset);
        }

        public Task SetLockoutEndDateAsync(User user, DateTimeOffset lockoutEnd)
        {
            throw new NotImplementedException();
        }

        public Task<int> IncrementAccessFailedCountAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task ResetAccessFailedCountAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetAccessFailedCountAsync(User user)
        {
            //TODO:  Can all this be handles cached, do I need a round trip every time?
            //return Task.FromResult(user.AccessFailedCount);

            var result = Users.FirstOrDefault(u => u.Id.Equals(user.Id));
            if (result == null)
                return Task.FromResult(-1);

            return Task.FromResult(result.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(User user)
        {
            var result = Users.FirstOrDefault(u => u.Id.Equals(user.Id));
            if (result == null)
                return Task.FromResult(false);

            return Task.FromResult(result.LockoutEnabled);
        }

        public Task SetLockoutEnabledAsync(User user, bool enabled)
        {
            var result = Users.FirstOrDefault(u => u.Id.Equals(user.Id));
            if (result == null)
            {
                return Task.FromResult(user.LockoutEnabled = enabled);
            }
            result.LockoutEnabled = enabled;
            return Task.FromResult(UnitOfWork.SaveAsync());
        }

        #endregion

        #region IUserTwoFactorStore Implementation

        public Task SetTwoFactorEnabledAsync(User user, bool enabled)
        {
            return Task.FromResult(false);
        }

        public Task<bool> GetTwoFactorEnabledAsync(User user)
        {
            return Task.FromResult(false);
        }

        #endregion

        #region IUserPhoneNumberStore

        public Task SetPhoneNumberAsync(User user, string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPhoneNumberAsync(User user)
        {
            var result = Users.FirstOrDefault(u => u.Id.Equals(user.Id));
            if (result == null)
                return Task.FromResult(string.Empty);

            return Task.FromResult(result.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task SetPhoneNumberConfirmedAsync(User user, bool confirmed)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDispose Method Implementation

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && Context != null)
            {
                Context.Dispose();
            }
        }

        #endregion
    }
}