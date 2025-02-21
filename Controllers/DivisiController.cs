using EmployeeCRUD.Data;
using EmployeeCRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DivisiController : ControllerBase
    {
        private readonly EmployeeContext _context;

        // Constructor untuk menerima instance dari EmployeeContext
        public DivisiController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: api/Divisi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Divisi>>> Getdivisi()
        {
            // Mengambil semua data divisi dari database
            var divisi = await _context.Divisions.ToListAsync();

            if (divisi == null || !divisi.Any())
            {
                return NotFound("No divisi found.");
            }

            return Ok(divisi); // Mengembalikan hasil dalam format OK
        }

        // GET: api/Divisi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Divisi>> GetDivisi(int id)
        {
            // Mencari divisi berdasarkan ID
            var divisi = await _context.Divisions.FindAsync(id);

            if (divisi == null)
            {
                return NotFound("Divisi not found.");
            }

            return Ok(divisi); // Mengembalikan divisi yang ditemukan
        }

        // POST: api/Divisi
        [HttpPost]
        public async Task<ActionResult<Divisi>> PostDivisi(Divisi divisi)
        {
            // Menambahkan divisi baru ke dalam konteks (database)
            _context.Divisions.Add(divisi);
            await _context.SaveChangesAsync();

            // Mengembalikan respons dengan status CreatedAtAction yang menciptakan resource baru
            return CreatedAtAction("GetDivisi", new { id = divisi.Id }, divisi);
        }

        // PUT: api/Divisi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDivisi(int id, Divisi divisi)
        {
            // Memastikan ID yang diterima sesuai dengan ID divisi
            if (id != divisi.Id)
            {
                return BadRequest("ID mismatch.");
            }

            // Menandai entity sebagai modified untuk update
            _context.Entry(divisi).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent(); // Mengembalikan status NoContent untuk menunjukkan bahwa update berhasil
        }

        // DELETE: api/Divisi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDivisi(int id)
        {
            // Mencari divisi berdasarkan ID
            var divisi = await _context.Divisions.FindAsync(id);
            if (divisi == null)
            {
                return NotFound("Divisi not found.");
            }

            // Menghapus divisi dari database
            _context.Divisions.Remove(divisi);
            await _context.SaveChangesAsync();

            return NoContent(); // Mengembalikan status NoContent setelah penghapusan berhasil
        }
    }
}
