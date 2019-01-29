using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommentApi.Models;

namespace CommentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly CommentContext _context;

        public CommentController(CommentContext context)
        {
            _context = context;

            if (_context.CommentItems.Count() == 0)
            {
                // Create a new CommentItem if collection is empty,
                // which means you can't delete all comments.
                _context.CommentItems.Add(new CommentItem { Name = "Comment1", IsPublic = true });

                _context.SaveChanges();
            }
        }

        // GET: api/Comment             **READ**
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentItem>>> GetCommentItem()
        {
            return await _context.CommentItems.ToListAsync();
        }

        // GET: api/Comment/5           **READ**
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentItem>> GetCommentItem(long id)
        {
            var commentItem = await _context.CommentItems.FindAsync(id);  // CommentItems is the collection in the db

            if (commentItem == null)
            {
                return NotFound();
            }

            return commentItem;

        }

        // POST: api/Comment            **CREATE**
        [HttpPost]
        public async Task<ActionResult<CommentItem>> PostCommentItem(CommentItem item)
        {
            _context.CommentItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCommentItem), new { id = item.Id }, item);
        }

        // PUT: api/Comment/5           **UPDATE**
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommentItem(long id, CommentItem item)
        {
            if (id != item.Id)  
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();

        }

        // DELETE: api/Comment/5        **DELETE**
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommentItem(long id)
        {
            var commentItem = await _context.CommentItems.FindAsync(id);

            if (commentItem == null)
            {
                return NotFound();
            }

            _context.CommentItems.Remove(commentItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }


}