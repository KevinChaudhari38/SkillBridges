using Microsoft.AspNetCore.Mvc.Rendering;

namespace SkillBridges.Models
{
    public interface IUserRepository
    {
        User GetByEmailAndPassword(string email, string password);
        User GetByEmail(string Email);
        IEnumerable<SelectListItem> GetRoles();
        User GetById(String id);
        void insert(User user);
        void update(User user);
        void delete(User user);
        List<User> GetAll();

 
    }
}
