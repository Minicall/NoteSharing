using System.ComponentModel.DataAnnotations.Schema;

namespace NoteSharing.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int NoteId { get; set; }

        public Note Note { get; set; }
    }
}
