using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public List<OrderDetail> OrderDetails { get; set; } = new();

        public decimal TotalAmount { get; set; }
        public string? Notes { get; set; }
        public bool isPay = false;
        public IdentityUser? User { get; set; }
    }
}
