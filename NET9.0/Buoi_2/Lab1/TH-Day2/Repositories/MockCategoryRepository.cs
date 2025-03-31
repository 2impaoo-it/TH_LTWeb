using TH_Day2.Models;

namespace TH_Day2.Repositories
{
    public class MockCategoryRepository : ICategoryRepository
    {
        private List<Category> _categoryList;
        public MockCategoryRepository() 
        {
            _categoryList = new List<Category>
            {
                new Category { Id = 1, Name = "Điện tử" },
                new Category { Id = 2, Name = "Thức uống" },
                new Category { Id = 3, Name = "Xe hơi" },
            };
        }
        public IEnumerable<Category> GetAllCategories()
        {
            return _categoryList;
        }
    }
}
