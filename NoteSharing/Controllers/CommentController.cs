using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NoteSharing.Data.Repositories;
using NoteSharing.Models;
using NoteSharing.Models.Request_Models;
using NoteSharing.Services;
using System.Xml.Linq;

namespace NoteSharing.Controllers
{
    [Route("api/note/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetCommentAsync(int id)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetAllCommentsAsync()
        {
            var comments = await _commentService.GetAllCommentsAsync();

            if (comments.IsNullOrEmpty())
            {
                return NotFound();
            }

            return Ok(comments);
        }

        [HttpGet("noteid/{noteId}")]
        public async Task<ActionResult<Comment>> GetCommentByNoteIdAsync(int noteId)
        {
            var comments = await _commentService.GetCommentsByNoteIdAsync(noteId);

            if (comments == null || !comments.Any())
            {
                return NotFound();
            }

            return Ok(comments);
        }

        [HttpPost]
        public async Task<ActionResult<Comment>> PostCommentsAsync(CommentRequest commentRequest)
        {
            Comment comment = new Comment()
            {
                Content = commentRequest.Content,
                NoteId = commentRequest.NoteId,
            };

            try
            {
                var createdComment = await _commentService.AddCommentAsync(comment);
                return CreatedAtAction(nameof(GetCommentAsync), new { id = createdComment.Id }, createdComment);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutComments([FromQuery] int id, CommentRequest commentRequest)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            comment.Content = commentRequest.Content;

            await _commentService.UpdateCommentAsync(comment);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteComments([FromQuery] int id)
        {
            await _commentService.DeleteCommentAsync(id);

            return NoContent();
        }
    }
}
