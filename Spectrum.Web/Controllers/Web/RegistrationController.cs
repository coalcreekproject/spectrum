using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        //public ApplicationUserManager UserManager
        //{
        //    get
        //    {
        //        return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    }
        //    private set
        //    {
        //        _userManager = value;
        //    }
        //}

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Account/Register
        //[AllowAnonymous]
        //public ActionResult Register()
        //{
        //    //var coreDbContext = new CoreDbContext();

        //    //var registerViewModel = new RegisterViewModel
        //    //{
        //    //    OrganizationTypes = coreDbContext.OrganizationTypes.ToList()
        //    //};

        //    //return View();
        //}

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
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
                            //TODO: Check for user name/email in use
                            await userRepository.CreateAsync(user);

                            //New Organization
                            //TODO: Check for an existing organization by org name and email
                            var organization = new Organization
                            {
                                Name = model.OrganizationName,
                                OrganizationTypeId = Convert.ToInt32(model.OrganizationTypeId)
                            };

                            var organizationRepository = new OrganizationRepository(uow);
                            organizationRepository.InsertOrUpdate(organization);
                            await organizationRepository.SaveAsync();

                            //Default organization profile and roles creation
                            AddOrganizationProfile(organization);
                            AddOrganizationRoles(organization);

                            organization.ObjectState = ObjectState.Modified;
                            organizationRepository.InsertOrUpdate(organization);
                            await organizationRepository.SaveAsync();

                            //Default user profile, roles and organization associations TODO: determine how to do admins
                            AddUserProfile(user, organization);
                            AddUserOrganization(user, organization);
                            AddUserRoles(user, organization);

                            user.ObjectState = ObjectState.Modified;
                            await userRepository.UpdateAsync(user);

                            dbContextTransaction.Commit();
                        }
                        catch (ApplicationException ex)
                        {
                            dbContextTransaction.Rollback();

                            //TODO: If there were problems, mine them from the exception and return the view
                            //AddErrors(result);

                            //TODO: Can we pass the model back?
                            RedirectToAction("Index");
                        }
                    }
                }
            }

            //If it drops through, no errors, sign in and redirect.
            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            // RedirectToAction("Index", "Portal");
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        private void AddUserRoles(User user, Organization organization)
        {
            user.UserRoles.Add(new UserRole
            {
                Default = true,
                OrganizationId = organization.Id,
                RoleId = organization.Roles.FirstOrDefault(r => r.Name.Equals("user")).Id,
                ObjectState = ObjectState.Added
            });
        }

        private void AddUserOrganization(User user, Organization organization)
        {
            user.UserOrganizations.Add(new UserOrganization
            {
                Default = true,
                ObjectState = ObjectState.Added,
                OrganizationId = organization.Id,
                UserId = user.Id
            });
        }

        private void AddUserProfile(User user, Organization organization)
        {
            user.UserProfiles.Add(new UserProfile
            {
                UserId = user.Id,
                OrganizationId = organization.Id,
                Default = true,
                ProfileName = "Default for " + user.UserName,
                ObjectState = ObjectState.Added
            });
        }

        private void AddOrganizationProfile(Organization organization)
        {
            organization.OrganizationProfiles.Add(new OrganizationProfile
            {
                ProfileName = "Default for " + organization.Name,
                Default = true,
                OrganizationId = organization.Id,
                ObjectState = ObjectState.Added
            });
        }

        private void AddOrganizationRoles(Organization organization)
        {
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

        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        [AllowAnonymous]
        public ActionResult Template(string template)
        {
            switch (template.ToLower())
            {
                case "registerindex":

                    var coreDbContext = new CoreDbContext();
                    var registerViewModel = new RegisterViewModel
                    {
                        //OrganizationTypes = coreDbContext.OrganizationTypes.ToList()
                    };

                    return PartialView("~/Views/Registration/Partials/RegistrationIndex.cshtml", registerViewModel);
                default:
                    throw new ApplicationException("Unknown Template");
            }
        }
    }
}