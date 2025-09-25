namespace SkillBridges.Models
{
    public interface ICategoryRepository
    {
        Category GetById(string id);
        List<Category> GetAll();
        void Insert(Category category);
        void Update(Category category);
        void Delete(Category category);

    }
}
