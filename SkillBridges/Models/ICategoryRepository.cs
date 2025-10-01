using Microsoft.AspNetCore.Mvc.Rendering;

namespace SkillBridges.Models
{
    public interface ICategoryRepository
    {
        Category GetById(string id);
        List<Category> GetAll();
        IEnumerable<SelectListItem> GetTypes();
        List<Category> GetByType(TaskType type);
        void Insert(Category category);
        void Update(Category category);
        void Delete(Category category);

    }
}
