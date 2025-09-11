using Microsoft.AspNetCore.Mvc.Rendering;

namespace SkillBridges.Models
{
    public interface IUserRepository
    {
        public User GetByEmailAndPassword(string email, string password);
        IEnumerable<SelectListItem> GetRoles();
        User GetById(int id);
        void insert(User user);
        List<User> GetAll();
    }
}
