using SkillBridges.Data;

namespace SkillBridges.Models
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly SkillBridgeContext _context;
        public CategoryRepository(SkillBridgeContext context)
        {
            _context = context;
        }
        public Category GetById(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.CategoryId == id);
        }
        public List<Category> GetAll()
        {
            return _context.Categories.ToList();
        }
        public void Insert(Category category)
        {
           
            _context.Categories.Add(category);
        }
        public void Update(Category category) { 
            _context.Categories.Update(category);
        }
        public void Delete(Category category) {
            _context.Categories.Remove(category);
        }
    }
}
