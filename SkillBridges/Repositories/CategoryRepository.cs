using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SkillBridges.Data;
using SkillBridges.Models;

namespace SkillBridges.Repositories
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly SkillBridgeContext _context;
        public CategoryRepository(SkillBridgeContext context)
        {
            _context = context;
        }
        public Category GetById(string id)
        {
            return _context.Categories.FirstOrDefault(c => c.CategoryId == id);
        }
        public List<Category> GetAll()
        {
            return _context.Categories.ToList();
        }
        public List<Category> GetByType(TaskType type)
        {
            return _context.Categories.Where(t=>t.type==type).ToList(); 
        }
        public IEnumerable<SelectListItem> GetTypes()
        {
            return Enum.GetValues(typeof(TaskType))
                       .Cast<TaskType>()
                       .Select(r => new SelectListItem
                       {
                           Value = ((int)r).ToString(),
                           Text = r.ToString()
                       });
        }
        public void Insert(Category category)
        {
            category.CategoryId = Guid.NewGuid().ToString();
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
