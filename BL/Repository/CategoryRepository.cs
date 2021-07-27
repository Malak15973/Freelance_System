using Freelance_System.BL.Interface;
using Freelance_System.DAL.Database;
using Freelance_System.Model;
using System.Linq;

namespace Freelance_System.BL.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DbContainer db;

        public CategoryRepository(DbContainer db)
        {
            this.db = db;
        }
        public IQueryable<CategoryVM> GetCategories()
        {
            return db.Category.Select(c => new CategoryVM { Id = c.Id,CategoryName=c.Name });
        }

        public CategoryVM GetCategoryById(int id)
        {
            return db.Category.Where(c=>c.Id==id).Select(c => new CategoryVM { Id = c.Id, CategoryName = c.Name }).FirstOrDefault(); 
        }
    }
}
