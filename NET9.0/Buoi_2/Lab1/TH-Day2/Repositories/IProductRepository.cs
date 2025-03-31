using System.Collections.Generic;
using TH_Day2.Models;

namespace TH_Day2.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        public void Add(Product product);
        public void Update(Product product);
        public void Delete(int id);
    }
}
