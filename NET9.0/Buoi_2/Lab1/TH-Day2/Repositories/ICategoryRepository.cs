using TH_Day2.Models;

namespace TH_Day2.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAllCategories();
    }
}
