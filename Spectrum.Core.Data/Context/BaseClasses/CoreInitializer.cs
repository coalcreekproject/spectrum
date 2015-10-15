using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectrum.Core.Data.Models;
using Microsoft.AspNet.Identity;

namespace Spectrum.Core.Data.Context.BaseClasses
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
                    UserName = "superusers@spectrumoperational.com",
                    Email = "superusers@spectrumoperational.com",
                    PasswordHash = hasher.HashPassword("password")
                },
                new User()
                {
                    UserName = "patrick.welch@spectrumoperational.com",
                    Email = "patrick.welch@spectrumoperational.com",
                    PasswordHash = hasher.HashPassword("password")
                }
            }.ForEach(u => context.Users.Add(u));

            context.SaveChanges();
            base.Seed(context);
        }
    }
}
