using System;

namespace Spectrum.Logic.Models
{
    [Serializable]
    public class ApplicationProfile
    {
        public int Id { get; set; } // Id (Primary key)
        public int ApplicationId { get; set; } // ApplicationId
        public string Description { get; set; } // Description
        public string Company { get; set; } // Company
        public string Author { get; set; } // Author
        public string License { get; set; } // License

        // Foreign keys
        public virtual ApplicationModel Application { get; set; }
    }

}
