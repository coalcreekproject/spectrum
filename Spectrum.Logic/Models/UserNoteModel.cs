using System;

namespace Spectrum.Logic.Models
{
    [Serializable]
    public class UserNoteModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Note { get; set; }

        public virtual UserModel UserModel { get; set; }
    }
}