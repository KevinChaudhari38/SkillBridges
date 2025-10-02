using Microsoft.AspNetCore.Mvc.Rendering;
using SkillBridges.Models;

namespace SkillBridges.Repositories
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
