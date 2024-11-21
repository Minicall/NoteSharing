using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteSharing.Data.Repositories;
using NoteSharing.Models;
using NoteSharing.Models.Request_Models;
using NoteSharing.Services;
using NuGet.Protocol.Core.Types;
using System;

namespace NoteSharing.Controllers
{
    [Route("api/note")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetAllNotesAsync()
        {
            var notes = await _noteService.GetAllNotesAsync();

            if (notes == null)
            {
                return NotFound();
            }

            return Ok(notes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetNoteByIdAsync(int id)
        {
            var note = await _noteService.GetNoteByIdAsync(id);
            if (note == null)
            {
                return NotFound();
            }

            return Ok(note);
        }

        [HttpPost]
        public async Task<ActionResult<Note>> PostNotes(NoteRequest noteReguest)
        {
            Note note = new Note()
            {
                Title = noteReguest.Title,
                Content = noteReguest.Content,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
            };
            await _noteService.AddNoteAsync(note);
            return CreatedAtAction(nameof(GetNoteByIdAsync), new { id = note.Id }, note);
        }

        [HttpPut]
        public async Task<IActionResult> PutNotes([FromQuery] int id, NoteRequest noteReguest)
        {
            var note = await _noteService.GetNoteByIdAsync(id);

            note.Title = noteReguest.Title;
            note.Content = noteReguest.Content;
            note.UpdatedAt = DateTime.Now;

            await _noteService.UpdateNoteAsync(note);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteNotes([FromQuery] int id)
        {
            await _noteService.DeleteNoteAsync(id);

            return NoContent();
        }
    }
}
