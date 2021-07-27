using Freelance_System.Model;
using System.Linq;

namespace Freelance_System.BL.Interface
{
    public interface ICategoryRepository
    {
        public IQueryable<CategoryVM> GetCategories();
        public CategoryVM GetCategoryById(int id);
    }
}
