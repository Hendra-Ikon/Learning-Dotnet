using InterviewTest.Data;
using InterviewTest.Factory;
using InterviewTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPostFactory _postFactory;

        public PostController(ApplicationDbContext context, IPostFactory postFactory)
        {
            _context = context;
            _postFactory = postFactory;
        }

        // 1️⃣ GET: Sinkronisasi data dari API eksternal
        [HttpPost("sync")]
        public async Task<IActionResult> SyncPosts()
        {
            var posts = await _postFactory.GetPosts();

            if (!posts.Any())
                return BadRequest("Tidak ada data yang bisa disimpan.");

            // Hanya simpan Id dan Title
            var filteredPosts = posts.Select(p => new Post { Id = p.Id, Title = p.Title }).ToList();

            _context.Posts.AddRange(filteredPosts);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Data berhasil disinkronisasi!", count = filteredPosts.Count });
        }

        // 2️⃣ GET: Ambil data dengan Pagination & Validasi
        [HttpGet]
        public async Task<IActionResult> GetPosts([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page < 1)
                return BadRequest(new { message = "Page tidak boleh kurang dari 1." });

            if (pageSize < 1 || pageSize > 100)
                return BadRequest(new { message = "PageSize harus antara 1 hingga 100." });

            var totalItems = await _context.Posts.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var posts = await _context.Posts
                .Select(p => new { p.Id, p.Title }) // Hanya ambil Id & Title
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            return Ok(new { page, pageSize, totalItems, totalPages, data = posts });
        }

        // 3️⃣ GET: Ambil satu data berdasarkan ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _context.Posts
                .Where(p => p.Id == id)
                .Select(p => new { p.Id, p.Title }) // Hanya ambil Id & Title
                .FirstOrDefaultAsync();

            if (post == null)
                return NotFound("Data tidak ditemukan.");

            return Ok(post);
        }

        // 4️⃣ POST: Tambah data manual
        [HttpPost]
        public async Task<IActionResult> CreatePost(Post post)
        {
            if (post == null)
                return BadRequest("Data tidak boleh kosong.");

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, new { post.Id, post.Title });
        }

        // 5️⃣ PUT: Update data berdasarkan ID
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, Post updatedPost)
        {
            if (id != updatedPost.Id)
                return BadRequest("ID tidak cocok.");

            var existingPost = await _context.Posts.FindAsync(id);
            if (existingPost == null)
                return NotFound("Data tidak ditemukan.");

            existingPost.Title = updatedPost.Title;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // 6️⃣ DELETE: Hapus data berdasarkan ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
                return NotFound("Data tidak ditemukan.");

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
