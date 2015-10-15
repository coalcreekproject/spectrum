using System.Data.Entity;
using Spectrum.Core.Data.Models.Interfaces;
using Spectrum.Core.Data.Models.ModelUtilities;

namespace Spectrum.Core.Data.Context.Extensions
{
    public static class ContextHelpers
    {
        public static void ApplyObjectStateChanges(this DbContext context)
        {
            foreach (var entry in context.ChangeTracker.Entries<IObjectState>())
            {
                IObjectState objectStateInfo = entry.Entity;
                entry.State = ObjectStateUtility.ConvertState(objectStateInfo.ObjectState);
            }
        }
    }
}
