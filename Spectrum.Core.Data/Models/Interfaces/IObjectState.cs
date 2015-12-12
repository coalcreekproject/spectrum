namespace Spectrum.Data.Core.Models.Interfaces
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
