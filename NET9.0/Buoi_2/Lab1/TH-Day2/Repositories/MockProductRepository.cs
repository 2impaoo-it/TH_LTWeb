using System.Collections.Generic;
using System.Linq;
using TH_Day2.Models;

namespace TH_Day2.Repositories
{
    public class MockProductRepository : IProductRepository
    {
        private readonly List<Product> _products;
        public MockProductRepository()
        {
            _products = new List<Product>
            {
                new Product { Id = 1, Name = "Laptop Acer Nitro 5", Price = 780, Description = "Laptop Acer Nitro V mang một thiết kế gaming mạnh mẽ, màn hình 15.6” 144Hz FHD, cấu hình cân mọi tựa game với chip i5-13420H từ Intel. Bên cạnh đó, dòng laptop này còn được tích hợp GPU GeForce RTX 2050, RAM 16GB và lưu trữ khổng lồ với SSD 512GB tốc độ cao giúp mang lại trải nghiệm vượt trội cho người dùng.", ImageUrl="/img/nitro5.jpg" },
                new Product { Id = 2, Name = "Iphone 16 Pro Max", Price = 1250, Description = "iPhone 16 Pro Max sở hữu màn hình Super Retina XDR OLED 6.9 inch với công nghệ ProMotion, mang lại trải nghiệm hiển thị mượt mà và sắc nét, lý tưởng cho giải trí và làm việc. Với chipset A18 Pro mạnh mẽ, mẫu iPhone đời mới này cung cấp hiệu suất vượt trội, giúp xử lý mượt mà các tác vụ nặng như chơi game hay edit video. Chiếc điện thoại iPhone 16 mới này còn sở hữu hệ thống camera Ultra Wide 48MP cho khả năng chụp ảnh cực kỳ chi tiết, mang đến chất lượng hình ảnh ấn tượng trong mọi tình huống.",ImageUrl="/img/ip16.jpg" },
                new Product { Id = 3, Name = "DJI Osmo Pocket 3", Price = 625, Description = "DJI Osmo Pocket 3 là sản phẩm camera hành động mới của DJI, có nhiều tính năng nâng cấp hiện đại hơn người anh em tiền nhiệm Pocket 3 của nó. Phiên bản mới sở hữu cảm biến CMOS 1 inch, giúp ghi lại các chi tiết trong điều kiện ánh sáng yếu tốt hơn, có khả năng quay video 4K/120fps, hỗ trợ chế độ màu D-Log M và HLG 10-bit, ghi lại phong cảnh với màu sắc chân thực, sinh động…", ImageUrl="/img/pocket3.jpg" },
                new Product { Id = 4, Name = "Sữa tươi trân châu đường đen", Price = 1, Description = "Sữa tươi trân châu đường đen là loại thức uống mới, có nguồn gốc từ Đài Loan. Món đồ uống này là sự kết hợp hoàn hảo giữa sữa tươi không đường và trân châu đường đen. Hương vị thanh mát nhẹ nhàng của sữa tươi hòa quyện với trân châu dẻo dai, ngọt ngào đã tạo nên sức hấp dẫn không thể chối từ của loại đồ uống này",ImageUrl="/img/sua-tuoi.jpg" },
                new Product { Id = 5, Name = "Honda Civic RS 2024", Price = 35151, Description = "Khối động cơ tăng áp 1.5L DOHC VTEC TURBO danh tiếng của thế hệ thứ 11. Sức mạnh cực đại mà cỗ máy này mang đến là 178 (mã lực) tại vòng tua 6.000 (vòng/phút), mô-men xoắn cực đại là 240 (Nm) tại dải vòng tua 1.700 – 4.500 (vòng/phút). Cả 3 phiên bản được hỗ trợ hộp số vô cấp CVT, ứng dụng Earth Dreams Technology, giúp chiếc xe vừa bứt phá mạnh mẽ nhưng vẫn tiết kiệm nhiên liệu.",ImageUrl="/img/civic.jpg" },
                new Product { Id = 6, Name = "Porsche 911", Price = 388666, Description = "911 là chiếc xe đầu bảng của Porsche, chiếc xe đã tạo nên danh tiếng của hãng xe Đức, với thiết kế trường tồn theo thời gian, cảm giác lái thể thao và tốc độ. 911 luôn nằm trong danh sách muốn sở hữu của rất nhiều người yêu xe trên toàn thế giới. ",ImageUrl="/img/911.jpg" },
            };
        }

        public IEnumerable<Product> GetAll()
        {
            return _products;
        }

        public Product GetById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public void Add(Product product)
        {

            _products.Add(product);
        }

        public void Update(Product product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Description = product.Description;
                existingProduct.CategoryId = product.CategoryId;
            }
        }

        public void Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _products.Remove(product);
            }
        }
    }
}
