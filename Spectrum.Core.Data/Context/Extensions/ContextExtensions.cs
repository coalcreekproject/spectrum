using System.Data.Entity;
using Spectrum.Data.Core.Models.Interfaces;
using Spectrum.Data.Core.Models.ModelUtilities;

namespace Spectrum.Data.Core.Context.Extensions
{
    public static class ContextExtensions
    {
        public static void ApplyObjectStateChanges(this DbContext context)
        {
            foreach (var entry in context.ChangeTracker.Entries<IObjectState>())
            {
                var objectStateInfo = entry.Entity;
                entry.State = ObjectStateUtility.ConvertState(objectStateInfo.ObjectState);
            }
        }
    }
}