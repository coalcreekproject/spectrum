using System;

namespace Spectrum.Logic.Models
{
    [Serializable]
    public class ApplicationNoteModel
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string Note { get; set; }

        public virtual ApplicationModel Application { get; set; }
    }
}