using Microsoft.AspNetCore.Mvc.Rendering;
using SkillBridges.Models;

namespace SkillBridges.Repositories
{
    public interface IUserRepository
    {
        User GetByEmailAndPassword(string email, string password);
        User GetByEmail(string Email);
        IEnumerable<SelectListItem> GetRoles();
        User GetById(string id);
        void insert(User user);
        void update(User user);
        void delete(User user);
        List<User> GetAll();

 
    }
}
