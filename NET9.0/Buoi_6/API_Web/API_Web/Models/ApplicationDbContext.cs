using Microsoft.EntityFrameworkCore;

namespace API_Web.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>
        options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}
