using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Spectrum.Data.Core.Context;
using Spectrum.Data.Core.Models;

namespace Spectrum.Data.Core.Repositories.Interfaces
{
    public interface IUserRepository : IQueryableUserStore<User, int>
    {
        CoreDbContext Context { get; }
    }
}