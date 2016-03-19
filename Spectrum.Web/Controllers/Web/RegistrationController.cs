using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Spectrum.Data.Core.Context;
using Spectrum.Data.Core.Context.UnitOfWork;
using Spectrum.Data.Core.Models;
using Spectrum.Data.Core.Models.Interfaces;
using Spectrum.Data.Core.Repositories;
using Spectrum.Web.IdentityConfig;
using Spectrum.Web.Models;

namespace Spectrum.Web.Controllers.Web
{
    public class RegistrationController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private OrganizationRepository _organizationRepository;
        private UserProfileRepository _userProfileRepository;
        private UserRepository _userRepository;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            var coreDbContext = new CoreDbContext();

            var registerViewModel = new RegisterViewModel
            {
                OrganizationTypes = coreDbContext.OrganizationTypes.ToList()
            };

            return View(registerViewModel);
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            User user = null;

            if (ModelState.IsValid)
            {
                using (var context = new CoreDbContext())
                {
                    //Make this whole thing transactional
                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            var uow = new CoreUnitOfWork(context);
                            var hasher = new PasswordHasher();

                            //New user
                            user = new User
                            {
                                UserName = model.Email,
                                Email = model.Email,
                                PasswordHash = hasher.HashPassword(model.Password)
                            };

                            var userRepository = new UserRepository(uow);
                            await userRepository.CreateAsync(user);

                            //New Organization
                            //TODO: Check for an existing organization by org name and email
                            var organization = new Organization
                            {
                                Name = model.OrganizationName,
                                OrganizationTypeId = Convert.ToInt32(model.OrganizationType)
                            };

                            var organizationRepository = new OrganizationRepository(uow);
                            organizationRepository.InsertOrUpdate(organization);
                            await organizationRepository.SaveAsync();

                            //Default organization profile creation
                            organization.OrganizationProfiles.Add(new OrganizationProfile
                            {
                                ProfileName = "Default for " + organization.Name,
                                Default = true,
                                OrganizationId = organization.Id,
                                ObjectState = ObjectState.Added
                            });

                            //Add standard organization roles
                            organization.Roles.Add(new Role
                            {
                                Name = "admin",
                                Description = "Administrator",
                                OrganizationId = organization.Id,
                                ObjectState = ObjectState.Added
                            });

                            organization.Roles.Add(new Role
                            {
                                Name = "user",
                                Description = "Standard user",
                                OrganizationId = organization.Id,
                                ObjectState = ObjectState.Added
                            });

                            organization.Roles.Add(new Role
                            {
                                Name = "observer",
                                Description = "Read only role",
                                OrganizationId = organization.Id,
                                ObjectState = ObjectState.Added
                            });

                            organization.ObjectState = ObjectState.Modified;
                            organizationRepository.InsertOrUpdate(organization);
                            await organizationRepository.SaveAsync();

                            //Default user profile creation
                            user.UserProfiles.Add(new UserProfile
                            {
                                UserId = user.Id,
                                OrganizationId = organization.Id,
                                Default = true,
                                ProfileName = "Default for " + user.UserName,
                                ObjectState = ObjectState.Added
                            });

                            //Add user organization assignment.
                            user.UserOrganizations.Add(new UserOrganization
                            {
                                Default = true,
                                ObjectState = ObjectState.Added,
                                OrganizationId = organization.Id,
                                UserId = user.Id
                            });

                            //Add a user role, in this case base user, TODO: determine how to do admins
                            user.UserRoles.Add(new UserRole
                            {
                                Default = true,
                                OrganizationId = organization.Id,
                                RoleId = organization.Roles.FirstOrDefault(r => r.Name.Equals("user")).Id,
                                ObjectState = ObjectState.Added
                            });

                            user.ObjectState = ObjectState.Modified;
                            await userRepository.UpdateAsync(user);

                            dbContextTransaction.Commit();
                        }
                        catch (ApplicationException ex)
                        {
                            dbContextTransaction.Rollback();
                            
                            //TODO: If there were problems, mine them from the exception and return the view
                            //AddErrors(result);
                            return View(model);
                        }
                    }
                }
            }

            //If it drops through, no errors, sign in and redirect.
            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            return RedirectToAction("Index", "Portal");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}