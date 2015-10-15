namespace Spectrum.Core.Data.Models.Interfaces
{
    public interface IObjectState
    {
        ObjectState ObjectState { get; set; }
    }

    public enum ObjectState
    {
        Added,
        Unchanged,
        Modified,
        Deleted
    }
}
