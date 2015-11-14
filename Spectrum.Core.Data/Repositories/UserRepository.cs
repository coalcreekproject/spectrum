using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Spectrum.Core.Data.Context;
using Spectrum.Core.Data.Context.UnitOfWork;
using Spectrum.Core.Data.Models;
using Spectrum.Core.Data.Models.Interfaces;

namespace Spectrum.Core.Data.Repositories
{
    public class UserRepository : IQueryableUserStore<User, int>,
        IUserPasswordStore<User, int>,
        IUserLoginStore<User, int>,
        IUserEmailStore<User, int>,
        IUserClaimStore<User, int>,
        IUserRoleStore<User, int>,
        IUserLockoutStore<User, int>,
        IUserTwoFactorStore<User, int>,
        IUserPhoneNumberStore<User, int>
    {

        private readonly CoreDbContext _context;
        private readonly ICoreUnitOfWork _coreUnitOfWork;

        public UserRepository()
        {
            _context = new CoreDbContext();
            _coreUnitOfWork = new CoreUnitOfWork(_context);
        }


        public UserRepository(CoreDbContext context)
        {
            _context = context;
            _coreUnitOfWork = new CoreUnitOfWork(_context);
        }

        public UserRepository(ICoreUnitOfWork uow)
        {
            _coreUnitOfWork = uow;
            _context = uow.Context;
        }

        public IQueryable<User> Users
        {
            get { return _context.Users; }
        }

        #region IUSerStore Implementation

        public Task CreateAsync(User user)
        {
            _context.Users.Add(user);
            return _coreUnitOfWork.SaveAsync();
        }

        public Task UpdateAsync(User user)
        {
            user.ObjectState = ObjectState.Modified;
            _context.Entry(user).State = EntityState.Modified;
            return _coreUnitOfWork.SaveAsync();
        }

        public Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            return _coreUnitOfWork.SaveAsync();
        }

        public Task<User> FindByIdAsync(int userId)
        {
            return Users.FirstOrDefaultAsync(u => u.Id.Equals(userId));
        }

        public Task<User> FindByNameAsync(string userName)
        {
            var result = Users.FirstOrDefault(u => u.UserName.Equals(userName));
            if (result == null)
                return Task.FromResult(new User());

            return Task.FromResult(result);
        }

        #endregion

        #region IUserPasswordStore Implementation

        public Task SetPasswordHashAsync(User user, string password)
        {
            //var result = Users.FirstOrDefault(u => u.Id == user.Id);
            //if (result == null)
            
            return Task.FromResult(user.PasswordHash = password);

            //user.PasswordHash = password;
            //_context.Entry(user).State = EntityState.Modified;
            
            //return _coreUnitOfWork.SaveAsync();
        }

        public Task<string> GetPasswordHashAsync(User user)
        {
            return Task.FromResult(_context.Users.Find(user.Id).PasswordHash);
            //var result = _context.Users.Find(user.Id).PasswordHash;
            //return Task.FromResult(result);
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            var password = _context.Users.Find(user.Id).PasswordHash;
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
            return Task.FromResult(_context.Users.Find(login.ProviderKey));
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
            _context.Users.Find(user).Email = email;
            return _coreUnitOfWork.SaveAsync();
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
            return Task.FromResult(_context.Users.Find(user).EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed)
        {
            _context.Users.Find(user).EmailConfirmed = confirmed;
            return _coreUnitOfWork.SaveAsync();
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
                user.UserClaims.Add(new UserClaim()
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
            var role = new Role {Name = roleName};

            if (!user.Roles.Contains(role))
            {
                user.Roles.Add(role);
            }

            return Task.FromResult(true);
        }

        public Task RemoveFromRoleAsync(User user, string roleName)
        {
            var roles = user.Roles;

            Role role = roles.Select(r => new Role()
            {
                Name = roleName
            }).FirstOrDefault();

            roles.Remove(role);
            return _coreUnitOfWork.SaveAsync();
        }

        public Task<IList<string>> GetRolesAsync(User user)
        {
            var roles = user.Roles;
            IList<string> results = roles.Select(role => role.Name).ToList();

            return Task.FromResult(results);
        }

        public Task<bool> IsInRoleAsync(User user, string roleName)
        {
            var roles = user.Roles;
            IList<string> results = roles.Select(role => role.Name).ToList();

            return Task.FromResult(results.Count > 0);
        }

        #endregion

        #region IUserLockoutStore Implementation

        public Task<DateTimeOffset> GetLockoutEndDateAsync(User user)
        {
            var result = _context.Users.Find(user.Id);

            if (result.LockoutEndDateUtc == null)
                return Task.FromResult(DateTimeOffset.UtcNow.AddHours(-1));
            
            DateTimeOffset offset = new DateTimeOffset((DateTime) result.LockoutEndDateUtc);

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
            else
            {
                result.LockoutEnabled = enabled;
                return Task.FromResult(_coreUnitOfWork.SaveAsync());
            }
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
            if (disposing && _context != null)
            {
                _context.Dispose();
            }
        }

        #endregion


    }
}
