using InterviewTest.Data;
using InterviewTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InterviewTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DataController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1️⃣ GET: Ambil semua data dengan pagination
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataModel>>> GetPosts([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page < 1 || pageSize < 1)
                return BadRequest("Page dan pageSize harus lebih besar dari 0.");

            var posts = await _context.Data
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(posts);
        }

        // 2️⃣ GET: Ambil satu data berdasarkan ID
        [HttpGet("{id}")]
        public async Task<ActionResult<DataModel>> GetPost(int id)
        {
            var post = await _context.Data.FindAsync(id);

            if (post == null)
                return NotFound("Data tidak ditemukan.");

            return Ok(post);
        }

        // 3️⃣ POST: Tambah data baru
        [HttpPost]
        public async Task<ActionResult<DataModel>> CreatePost(DataModel post)
        {
            if (post == null)
                return BadRequest("Data tidak boleh kosong.");

            _context.Data.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
        }

        // 4️⃣ PUT: Update data berdasarkan ID
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, DataModel updatedPost)
        {
            if (id != updatedPost.Id)
                return BadRequest("ID tidak cocok.");

            var existingPost = await _context.Data.FindAsync(id);
            if (existingPost == null)
                return NotFound("Data tidak ditemukan.");

            existingPost.UserId = updatedPost.UserId;
            existingPost.Title = updatedPost.Title;
            existingPost.Body = updatedPost.Body;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // 5️⃣ DELETE: Hapus data berdasarkan ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.Data.FindAsync(id);
            if (post == null)
                return NotFound("Data tidak ditemukan.");

            _context.Data.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
