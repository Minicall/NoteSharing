namespace NoteSharing.Models.Request_Models
{
    public class NoteRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
