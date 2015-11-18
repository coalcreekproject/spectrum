﻿using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Spectrum.Core.Data.Models;

namespace Spectrum.Core.Data.Context.Initializers
{
    class CoreInitializer : CreateDatabaseIfNotExists<CoreDbContext>
    {
        protected override void Seed(CoreDbContext context)
        {
            PasswordHasher hasher = new PasswordHasher();

            new List<User>
            {
                new User()
                {
                    UserName = "superuser",
                    Email = "superusers@spectrumoperational.com",
                    PasswordHash = hasher.HashPassword("p@ssw0rd")
                },
                new User()
                {
                    UserName = "Patrick Welch",
                    Email = "patrick.welch@spectrumoperational.com",
                    PasswordHash = hasher.HashPassword("p@ssw0rd")
                }
            }.ForEach(u => context.Users.Add(u));

            new List<OrganizationType>
            {
                new OrganizationType()
                {
                    Id = 1,
                    Name = "Local Government",
                    Description = "Municipality, city, town or township, bourough that has corporate status and local government"
                },
                new OrganizationType()
                {
                    Id = 2,
                    Name = "County Government",
                    Description = "County governments"
                },
                new OrganizationType()
                {
                    Id = 3,
                    Name = "State Government",
                    Description = "State governments"
                },
                new OrganizationType()
                {
                    Id = 4,
                    Name = "Federal Government",
                    Description = "Federal government"
                },
                new OrganizationType()
                {
                    Id = 5,
                    Name = "Non-government Organization",
                    Description = "Non-government organization (example: Red Cross)"
                },
                new OrganizationType()
                {
                    Id = 6,
                    Name = "Hospital",
                    Description = "Hospitals"
                },
                new OrganizationType()
                {
                    Id = 7,
                    Name = "Private",
                    Description = "Private companies and organizations"
                },
            }.ForEach(t => context.OrganizationTypes.Add(t));


            context.SaveChanges();
            base.Seed(context);
        }
    }
}
