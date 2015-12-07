using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spectrum.Core.Data.Context;
using Spectrum.Core.Data.Context.UnitOfWork;
using Spectrum.Core.Data.Models;
using Spectrum.Core.Data.Repositories;

namespace SpectrumEm.Data.Core.Tests.Stores
{
    [TestClass()]
    public class ApplicationUserStoreTests
    {

        [TestMethod()]
        public void UserStoreTest()
        {
            var applicationUserStore = new UserRepository(new CoreUnitOfWork(new CoreDbContext()));
            var users = applicationUserStore.Users;
            Assert.IsTrue(users.Count() == 2);
        }

        [TestMethod()]
        public async Task CreateAsyncTest()
        {
            var applicationUserStore = new UserRepository(new CoreUnitOfWork(new CoreDbContext()));

            var testUser = new User()
            {
                AccessFailedCount = 0,
                Email = "pwelch@foo.com",
                EmailConfirmed = false,
                LockoutEnabled = false,
                LockoutEndDateUtc = null,
                PhoneNumber = "000-000-0000",
                UserName = "pwelch22",
            };

            await applicationUserStore.CreateAsync(testUser);
        }

        [TestMethod()]
        public async Task CreateAsyncWithNewOrgTest()
        {
            var applicationUserStore = new UserRepository(new CoreUnitOfWork(new CoreDbContext()));

            var testUser = new User()
            {
                AccessFailedCount = 0,
                Email = "pwelch@foo.com",
                EmailConfirmed = false,
                LockoutEnabled = false,
                LockoutEndDateUtc = null,
                PhoneNumber = "000-000-0000",
                UserName = "pwelch23",
                //UserOrganizations = new Collection<UserOrganization>() { CreateOrganization() }
            };

            await applicationUserStore.CreateAsync(testUser);
        }

        [TestMethod()]
        public async Task CreateAsyncWithExistingOrgTest()
        {
            var applicationUserStore = new UserRepository(new CoreUnitOfWork(new CoreDbContext()));

            var testUser = new User()
            {
                AccessFailedCount = 0,
                Email = "pwelch@foo.com",
                EmailConfirmed = false,
                LockoutEnabled = false,
                LockoutEndDateUtc = null,
                PhoneNumber = "000-000-0000",
                UserName = "pwelch22",
                //Organizations = new Collection<Organization>() { CreateOrganization() }
            };

            await applicationUserStore.CreateAsync(testUser);
        }

        [TestMethod()]
        public async Task CreateAsyncWithNewDetailTest()
        {
            var applicationUserStore = new UserRepository(new CoreUnitOfWork(new CoreDbContext()));

            var testUser = new User()
            {
                AccessFailedCount = 0,
                Email = "pwelch@foo.com",
                EmailConfirmed = false,
                LockoutEnabled = false,
                LockoutEndDateUtc = null,
                PhoneNumber = "000-000-0000",
                UserName = "pwelch24", 
                UserProfiles = new Collection<UserProfile>() { CreateUserDetail() }
            };

            await applicationUserStore.CreateAsync(testUser);
        }


        [TestMethod()]
        public async Task UpdateAsyncTest()
        {
            var applicationUserStore = new UserRepository(new CoreUnitOfWork(new CoreDbContext()));

            var result = await applicationUserStore.FindByIdAsync(3);
            result.EmailConfirmed = true;

            await applicationUserStore.UpdateAsync(result);

            result = await applicationUserStore.FindByIdAsync(3);
            Assert.IsTrue(result.EmailConfirmed);
        }

        [TestMethod()]
        public async Task DeleteAsyncTest()
        {
            var applicationUserStore = new UserRepository(new CoreUnitOfWork(new CoreDbContext()));

            await applicationUserStore.CreateAsync(CreateUser());
            var result = await applicationUserStore.FindByNameAsync("pwelchThree");

            await applicationUserStore.DeleteAsync(result);

            result = await applicationUserStore.FindByNameAsync("pwelchThree");
            Assert.IsNull(result);
        }

        [TestMethod()]
        public async Task FindByIdAsyncTest()
        {
            var applicationUserStore = new UserRepository(new CoreUnitOfWork(new CoreDbContext()));
            var result = await applicationUserStore.FindByIdAsync(6);
            Assert.AreEqual("pwelch", result.UserName);
        }

        [TestMethod()]
        public async Task FindByNameAsyncTest()
        {
            var applicationUserStore = new UserRepository(new CoreUnitOfWork(new CoreDbContext()));
            var result = await applicationUserStore.FindByNameAsync("pwelch");
            Assert.AreEqual(1, result.Id);
        }

        [TestMethod()]
        public void SetPasswordHashAsyncTest()
        {

        }

        [TestMethod()]
        public void GetPasswordHashAsyncTest()
        {

        }

        [TestMethod()]
        public async Task HasPasswordAsyncTest()
        {
            var applicationUserStore = new UserRepository(new CoreUnitOfWork(new CoreDbContext()));
            var resultpos = await applicationUserStore.FindByIdAsync(1);
            var resultneg = await applicationUserStore.FindByIdAsync(3);

            Assert.IsTrue(await applicationUserStore.HasPasswordAsync(resultpos));
            Assert.IsFalse(await applicationUserStore.HasPasswordAsync(resultneg));
        }


        #region Helper Methods

        private Organization GetOrganization(int id)
        {
            return new Organization();
        }


        private User CreateUser()
        {
            var testUser = new User()
            {
                AccessFailedCount = 0,
                Email = "pwelchThree@foo.com",
                EmailConfirmed = false,
                LockoutEnabled = false,
                LockoutEndDateUtc = null,
                PhoneNumber = "303-000-0000",
                UserName = "pwelch100"
            };

            return testUser;
        }

        private UserProfile CreateUserDetail()
        {
            var testUserDetail = new UserProfile()
            {
                FirstName = "Lawrence",
                MiddleName = "Patrick",
                LastName = "Welch",
                Nickname = "Patrick",
                Title = "Emergency Manager"
            };

            return testUserDetail;
        }


        /// <summary>
        /// Helper
        /// </summary>
        /// <returns></returns>
        private OrganizationProfile CreateOrganizationDetail()
        {
            var organizationDetail = new OrganizationProfile()
            {
                ProfileName = "New County Office of Emergency Management",
                Default = true,
                Description = "Test Detail Record",
                Organization = CreateOrganization(),
            };

            return organizationDetail;
        }

        private Collection<OrganizationProfile> CreateOrganizationProfile()
        {
            var organizationProfiles = new Collection<OrganizationProfile>();
            organizationProfiles.Add(CreateOrganizationDetail());

            return organizationProfiles;
        }

        /// <summary>
        /// Helper
        /// </summary>
        /// <returns></returns>
        private Organization CreateOrganization()
        {
            var organization = new Organization()
            {
                Name = "New County Colorado OEM",
            };

            return organization;
        }

        /// <summary>
        /// Helper
        /// </summary>
        /// <returns></returns>
        private Collection<Organization> CreateOrganizationCollection()
        {
            var organizations = new Collection<Organization>();
            organizations.Add(CreateOrganization());

            return organizations;
        }

        #endregion

        #region Debug Methods

        /*

        /// <summary>
        /// This is the best method for debugging validation errors
        /// </summary>
        /// <returns></returns>
        [TestMethod()]
        public async Task ExampleMethodTest()
        {
            try
            {
                var applicationUserStore = new ApplicationUserStore(new CoreDbContext());
                //Do some wrork here
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error {1}", validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                }
            }
        }
        */

        #endregion
    }
}
