using System;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Context.Initializers
{
    internal class CoreInitializer : CreateDatabaseIfNotExists<CoreDbContext>
    {
        protected override void Seed(CoreDbContext context)
        {
            var hasher = new PasswordHasher();

            new List<OrganizationType>
            {
                new OrganizationType
                {
                    Id = 1,
                    Name = "Local Government",
                    Description =
                        "Municipality, city, town or township, bourough that has corporate status and local government"
                },
                new OrganizationType
                {
                    Id = 2,
                    Name = "County Government",
                    Description = "County governments"
                },
                new OrganizationType
                {
                    Id = 3,
                    Name = "State Government",
                    Description = "State governments"
                },
                new OrganizationType
                {
                    Id = 4,
                    Name = "Federal Government",
                    Description = "Federal government"
                },
                new OrganizationType
                {
                    Id = 5,
                    Name = "Non-government Organization",
                    Description = "Non-government organization (example: Red Cross)"
                },
                new OrganizationType
                {
                    Id = 6,
                    Name = "Hospital",
                    Description = "Hospitals"
                },
                new OrganizationType
                {
                    Id = 7,
                    Name = "Agency - First Responder",
                    Description = "First responder Agencies, Fire, EMS"
                },
                new OrganizationType
                {
                    Id = 8,
                    Name = "Agency - Law Enforcement",
                    Description = "LE Organizations, (example: City and State Police)"
                },
                new OrganizationType
                {
                    Id = 9,
                    Name = "Agency - Investigative",
                    Description = "Investigative Agencies"
                },
                new OrganizationType
                {
                    Id = 10,
                    Name = "Agency - Other",
                    Description = "Other Agencies"
                },
                new OrganizationType
                {
                    Id = 11,
                    Name = "Private",
                    Description = "Private companies and organizations"
                },
                new OrganizationType
                {
                    Id = 12,
                    Name = "Individual",
                    Description = "Private citizen or individual"
                }
            }.ForEach(t => context.OrganizationTypes.Add(t));

            //new List<Organization>
            //{
            //    new Organization
            //    {
            //        Name = "Spectrum Operational",
            //        OrganizationTypeId = 11,
            //    }
            //}.ForEach(o => context.Organizations.Add(o));


            var o = new Organization()
            {
                Name = "Spectrum Operational",
                OrganizationTypeId = 11,
            };

            o.OrganizationProfiles.Add(new OrganizationProfile()
            {
                Default = true,
                ProfileName = " Default - Spectrum Operational LLC",
                Description = "Profile created at database seed",
                PrimaryContact = "Patrick Welch",
                Phone = "303-704-2500",
                Email = "patrick.welch@spectrumoperational.com",
                County = "Boulder",
                Country = "United States",
                TimeZone = "US Mountain",
                DstAdjust = true,
                Language = "US English"
            });

            o.Roles.Add(new Role() { Name = "superuser", Description = "God mode", OrganizationId = 1 });
            o.Roles.Add(new Role() {Name = "admin", Description = "Administrator", OrganizationId = 1});
            o.Roles.Add(new Role() { Name = "user", Description = "Standard user", OrganizationId = 1 });
            o.Roles.Add(new Role() { Name = "observer", Description = "Read only role", OrganizationId = 1 });

            context.Organizations.Add(o);
            
            var superuser = new User
            {
                UserName = "superuser",
                Email = "superusers@spectrumoperational.com",
                PasswordHash = hasher.HashPassword("p@ssw0rd")
            };

            var pwelch = new User
            {
                UserName = "pwelch",
                Email = "patrick.welch@spectrumoperational.com",
                PasswordHash = hasher.HashPassword("p@ssw0rd")
            };

            var address = new Address()
            {
                Name = "Office",
                Default = true,
                Description = "Home Office of Spectrum Operational LLC",
                StreetOne = "2770 Arapahoe Road",
                StreetTwo = "Suite 132-113",
                City = "Lafayette",
                State = "CO",
                Zip = "80026"
            };

            var pwelchProfile = new UserProfile()
            {
                OrganizationId = 1,  //Wanna bet?
                Default = true,
                ProfileName = "Default for Patrick Welch, Spectrum Operational LLC",
                FirstName = "Lawrence",
                MiddleName = "Patrick",
                LastName = "Welch",
                Nickname = "Patrick",
                SecondaryEmail = "patrick_welch@comcast.net",
                SecondaryPhoneNumber = "720-282-5144",
                TimeZone = "US Mountain",
                DstAdjust = true,
            };

            var superUserProfile = new UserProfile()
            {
                OrganizationId = 1,
                Default = true,
                ProfileName = "Default for superuser, Spectrum Operational LLC",
                FirstName = "System",
                MiddleName = "System",
                LastName = "System",
                Nickname = "System",
                SecondaryEmail = "support@spectrumoperational.com",
                //SecondaryPhoneNumber = "",
                TimeZone = "US Mountain",
                DstAdjust = true,
            };

            UserProfileAddress pwelchUserProfileAddress = new UserProfileAddress();
            pwelchUserProfileAddress.Address = address;
            pwelchUserProfileAddress.UserProfile = pwelchProfile;

            pwelchProfile.UserProfileAddresses.Add(pwelchUserProfileAddress);
            pwelch.UserProfiles.Add(pwelchProfile);

            UserProfileAddress superuserUserProfileAddress = new UserProfileAddress();
            superuserUserProfileAddress.Address = address;
            superuserUserProfileAddress.UserProfile = superUserProfile;

            superUserProfile.UserProfileAddresses.Add(superuserUserProfileAddress);
            superuser.UserProfiles.Add(superUserProfile);

            context.Users.Add(pwelch);
            context.Users.Add(superuser);

            context.SaveChanges();
            base.Seed(context);
        }
    }
}