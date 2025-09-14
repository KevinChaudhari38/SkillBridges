namespace SkillBridges.Models
{
    public interface ICategoryRepository
    {
        Category GetById(int id);
        List<Category> GetAll();
        void Insert(Category category);
        void Update(Category category);
        void Delete(Category category);

    }
}
