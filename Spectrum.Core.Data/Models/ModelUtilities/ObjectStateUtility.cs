using System.Data.Entity;
using Spectrum.Data.Core.Models.Interfaces;

namespace Spectrum.Data.Core.Models.ModelUtilities
{
    internal class ObjectStateUtility
    {
        public static EntityState ConvertState(ObjectState objectState)
        {
            switch (objectState)
            {
                case ObjectState.Added:
                    return EntityState.Added;
                case ObjectState.Modified:
                    return EntityState.Modified;
                case ObjectState.Deleted:
                    return EntityState.Deleted;
                default:
                    return EntityState.Unchanged;
            }
        }
    }
}