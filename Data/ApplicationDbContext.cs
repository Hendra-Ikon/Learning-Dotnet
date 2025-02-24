using InterviewTest.Models;
using Microsoft.EntityFrameworkCore;

namespace InterviewTest.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        public DbSet<DataModel> Data { get; set; } // Pastikan model sudah dibuat
         public DbSet<Post> Posts { get; set; }
    }
}
