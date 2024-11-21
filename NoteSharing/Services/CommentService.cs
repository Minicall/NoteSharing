using NoteSharing.Data.Repositories;
using NoteSharing.Models;

namespace NoteSharing.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetAllCommentsAsync();
        Task<Comment> GetCommentByIdAsync(int id);
        Task<IEnumerable<Comment>> GetCommentsByNoteIdAsync(int noteId);
        Task<Comment> AddCommentAsync(Comment comment);
        Task<Comment> UpdateCommentAsync(Comment comment);
        Task DeleteCommentAsync(int id);
    }

    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> _commentRepository;

        public CommentService(IRepository<Comment> commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsAsync()
        {
            return await _commentRepository.GetAllAsync();
        }

        public async Task<Comment> GetCommentByIdAsync(int id)
        {
            return await _commentRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Comment>> GetCommentsByNoteIdAsync(int noteId)
        {
            var comments = await _commentRepository.GetAllAsync();
            var commentsForNote = new List<Comment>();

            foreach (var comment in comments)
            {
                if (comment.NoteId == noteId)
                {
                    commentsForNote.Add(comment);
                }
            }

            return commentsForNote;
        }

        public async Task<Comment> AddCommentAsync(Comment comment)
        {
            if (comment.NoteId == 0)
            {
                throw new ArgumentException("NoteId is required.", nameof(comment));
            }

            return await _commentRepository.AddAsync(comment);
        }

        public async Task<Comment> UpdateCommentAsync(Comment comment)
        {
            return await _commentRepository.UpdateAsync(comment);
        }

        public async Task DeleteCommentAsync(int id)
        {
            await _commentRepository.DeleteAsync(id);
        }
    }
}