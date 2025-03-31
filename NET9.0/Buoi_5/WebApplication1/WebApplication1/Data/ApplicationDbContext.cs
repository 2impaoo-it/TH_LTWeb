using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    public DbSet<WebApplication1.Models.CartItem> CartItem { get; set; } = default!;
    public DbSet<WebApplication1.Models.Order> Order { get; set; } = default!;
    public DbSet<WebApplication1.Models.OrderDetail> OrderDetail { get; set; } = default!;
}
